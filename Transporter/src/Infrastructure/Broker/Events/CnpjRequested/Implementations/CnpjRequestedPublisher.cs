using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using src.Infrastructure.Broker.Events.CnpjRequested.Interfaces;
using src.Infrastructure.Broker.Events.CnpjRequested.Models;

namespace src.Infrastructure.Broker.Events.CnpjRequested.Implementations
{
    public class CnpjRequestedPublisher : ICnpjRequestedPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public CnpjRequestedPublisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "onway.cnpj", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "cnpj_consult_requested", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "cnpj_consult_requested", exchange: "onway.cnpj", routingKey: "cnpj.consult");
        }

        public Task Publish(long transporterId, string cnpj, string email)
        {
            var consult = new ConsultRequest
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Transporter_ID = transporterId,
                Email = email,
                CNPJ = cnpj
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(consult));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = consult.CorrelationId;
            try
            {
                _channel.BasicPublish(exchange: "onway.cnpj", routingKey: "cnpj.consult", basicProperties: properties, body: body);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}