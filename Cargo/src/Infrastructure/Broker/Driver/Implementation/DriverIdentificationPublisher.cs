using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using src.Infrastructure.Broker.Driver.Interface;
using src.Infrastructure.Broker.Driver.Messages;



namespace src.Infrastructure.Broker.Driver.Implementation
{
    public class DriverIdentificationPublisher : IDriverIdentificationPublisher
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;

        public DriverIdentificationPublisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "driver-identification-request", durable: true, exclusive: false, autoDelete: false, arguments: null);

        }
        public Task PublishAsync(long transporterId, long driverId)
        {
            var message = new DriverRequest
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Transporter_ID = transporterId,
                Driver_ID = driverId
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.ReplyTo = "driver-identification-response";
            _channel.BasicPublish(exchange: "", routingKey: "driver-identification-request", basicProperties: properties, body: body);
            return Task.CompletedTask;

        }
    }
}