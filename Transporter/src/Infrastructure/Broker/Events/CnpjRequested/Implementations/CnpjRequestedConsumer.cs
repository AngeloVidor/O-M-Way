using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using src.Application.Common.CnpjProcessing.Abstractions;
using src.Application.UseCases.ConsultCNPJ.Interfaces;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Application.UseCases.SendVerificationCodeToEmail.Interfaces;
using src.Application.UseCases.TemporaryData.Interfaces;
using src.Infrastructure.Broker.Events.CnpjRequested.Interfaces;
using src.Infrastructure.Broker.Events.CnpjRequested.Models;

namespace src.Infrastructure.Broker.Events.CnpjRequested.Implementations
{
    public class CnpjRequestedConsumer : ICnpjRequestedConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ISharedCnpjList _sharedCnpjList;
        private readonly ITransporterTemporaryDataService _transporterTemporaryDataService;
        private readonly IConsultCnpjService _consultCnpjService;
        private readonly ILogger<CnpjRequestedConsumer> _logger;
        private readonly IVerificationCodeHandler _verificationCode;
        private readonly ISendVerificationCodeToEmailService _sendVerificationCodeToEmailService;
        private readonly IServiceScopeFactory _serviceScopeFactory;



        public CnpjRequestedConsumer(ISharedCnpjList sharedCnpjList, ITransporterTemporaryDataService transporterTemporaryDataService, IConsultCnpjService consultCnpjService, ILogger<CnpjRequestedConsumer> logger, IVerificationCodeHandler verificationCode, ISendVerificationCodeToEmailService sendVerificationCodeToEmailService, IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "onway.cnpj", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "cnpj_consult_requested", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "cnpj_consult_requested", exchange: "onway.cnpj", routingKey: "cnpj.consult");

            _sharedCnpjList = sharedCnpjList;
            _transporterTemporaryDataService = transporterTemporaryDataService;
            _consultCnpjService = consultCnpjService;
            _logger = logger;
            _verificationCode = verificationCode;
            _sendVerificationCodeToEmailService = sendVerificationCodeToEmailService;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var sharedCnpjList = scope.ServiceProvider.GetRequiredService<ISharedCnpjList>();
                var transporterTemporaryDataService = scope.ServiceProvider.GetRequiredService<ITransporterTemporaryDataService>();
                var consultCnpjService = scope.ServiceProvider.GetRequiredService<IConsultCnpjService>();
                var verificationCodeHandler = scope.ServiceProvider.GetRequiredService<IVerificationCodeHandler>();
                var sendVerificationCodeToEmailService = scope.ServiceProvider.GetRequiredService<ISendVerificationCodeToEmailService>();

                var body = ea.Body.ToArray();
                var requestBody = Encoding.UTF8.GetString(body);
                var consultObject = JsonConvert.DeserializeObject<ConsultRequest>(requestBody);

                try
                {
                    if (consultObject != null)
                    {
                        var response = await sharedCnpjList.HandleAsync(consultObject);
                        bool isValid = await consultCnpjService.IsCnpjValidAsync(response.CNPJ);

                        await transporterTemporaryDataService.UpdateCnpjValuesAsync(response.Transporter_ID, isValid);

                        var verificationCode = await verificationCodeHandler.GenerateCodeAsync(consultObject.Email);
                        bool updatedVerificationCode = await transporterTemporaryDataService.UpdateVerificationCodeAsync(response.Transporter_ID, verificationCode.Code);
                        if (!updatedVerificationCode)
                        {
                            throw new Exception("Failed to update verification code in the database.");
                        }

                        await sendVerificationCodeToEmailService.SentAsync(
                            verificationCode.Email,
                            verificationCode.Code,
                            verificationCode.CreatedAt,
                            verificationCode.ExpirationDate
                        );
                    }

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing the message: {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _channel.BasicConsume(queue: "cnpj_consult_requested", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

    }
}