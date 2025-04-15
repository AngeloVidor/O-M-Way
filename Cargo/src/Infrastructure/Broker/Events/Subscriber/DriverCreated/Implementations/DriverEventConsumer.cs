using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using src.Application.UseCases.DriverSnapshots.Interfaces;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Interfaces;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Models;

namespace src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Implementations
{
    public class DriverEventConsumer : IDriverEventConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IDriverSnapshotService _driverSnapshotService;
        public DriverEventConsumer(IDriverSnapshotService driverSnapshotService)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "onway.driver", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "driver_created_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "driver_created_queue", exchange: "onway.driver", routingKey: "driver.created");

            _driverSnapshotService = driverSnapshotService;
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
                    string message = Encoding.UTF8.GetString(body);
                    var driverCreatedEvent = JsonConvert.DeserializeObject<DriverCreatedEvent>(message);
                    await _driverSnapshotService.SaveCopyAsync(driverCreatedEvent);
                    _channel.BasicAck(ea.DeliveryTag, false);


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error consuming the message: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };
            _channel.BasicConsume(queue: "driver_created_queue", autoAck: false, consumer: consumer);
        }
    }
}