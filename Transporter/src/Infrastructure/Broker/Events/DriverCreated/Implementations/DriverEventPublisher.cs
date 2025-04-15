using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using src.Infrastructure.Broker.Events.DriverCreated.Models;
using src.Infrastructure.Broker.Events.DriverCreated.Interfaces;

namespace src.Infrastructure.Broker.Events.DriverCreated.Implementations
{
    public class DriverEventPublisher : IDriverEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public DriverEventPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "onway.driver", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "driver_created_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "driver_created_queue", exchange: "onway.driver", routingKey: "driver.created");
        }

        public Task Publish(long transporterId, long employeeId, string username)
        {
            var message = new DriverCreatedEvent
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Transporter_ID = transporterId,
                Employee_ID = employeeId,
                Username = username
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = message.CorrelationId;
            try
            {
                _channel.BasicPublish(exchange: "onway.driver", routingKey: "driver.created", basicProperties: properties, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }

}
