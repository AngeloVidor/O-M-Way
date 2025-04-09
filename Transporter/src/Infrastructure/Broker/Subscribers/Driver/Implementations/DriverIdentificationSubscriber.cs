using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using src.API.DTOs;
using src.Application.UseCases.Employees.Interfaces;
using src.Infrastructure.Broker.Subscribers.Driver.Interfaces;
using src.Infrastructure.Broker.Subscribers.Driver.Messages;

namespace src.Infrastructure.Broker.Subscribers.Driver.Implementations
{
    public class DriverIdentificationSubscriber : IDriverIdentificationSubscriber
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private readonly IEmployeerManagerService _employeerManagerService;
        public DriverIdentificationSubscriber(IEmployeerManagerService employeerManagerService)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "driver-identification-request", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "driver-identification-response", durable: true, exclusive: false, autoDelete: false, arguments: null);


            _employeerManagerService = employeerManagerService;
        }

        public void Consume()
        {
            Console.WriteLine("Listening...");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var response = JsonConvert.DeserializeObject<DriverRequest>(message);

                    var validateResponse = await ValidateDriverIdentification(response.Transporter_ID, response.Driver_ID);
                    var messageResponse = new DriverResponse
                    {
                        Driver_ID = validateResponse?.Employee_ID ?? 0,
                        Transporter_ID = validateResponse?.Transporter_ID ?? 0,
                        CorrelationId = response.CorrelationId,
                        Success = validateResponse != null
                    };
                    var replyTo = ea.BasicProperties.ReplyTo;
                    await Publish(messageResponse, replyTo);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Failed to process message: {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }

            };
            _channel.BasicConsume(queue: "driver-identification-request", autoAck: false, consumer: consumer);
        }

        public async Task<EmployeeDTO> ValidateDriverIdentification(long transporterId, long driverId)
        {
            return await _employeerManagerService.FindDriverInTransporterAsync(transporterId, driverId);
        }

        public Task Publish(DriverResponse driverResponse, string replyTo)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(driverResponse));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = driverResponse.CorrelationId;
            properties.ReplyTo = replyTo;
            try
            {
                _channel.BasicPublish(exchange: "", routingKey: replyTo, basicProperties: properties, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing message: {ex.Message}");
            }
            return Task.CompletedTask;

        }
    }
}