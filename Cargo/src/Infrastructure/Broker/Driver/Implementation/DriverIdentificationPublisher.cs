using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using src.Infrastructure.Broker.Driver.Interface;
using src.Infrastructure.Broker.Driver.Messages;



namespace src.Infrastructure.Broker.Driver.Implementation
{
    public class DriverIdentificationPublisher : IDriverIdentificationPublisher
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<DriverResponse>> _pendingRequests = new ConcurrentDictionary<string, TaskCompletionSource<DriverResponse>>();

        public DriverIdentificationPublisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "driver-identification-request", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "driver-identification-response", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var response = JsonConvert.DeserializeObject<DriverResponse>(message);

                if (_pendingRequests.TryGetValue(response.CorrelationId, out var tcs))
                {
                    tcs.TrySetResult(response);
                    _pendingRequests.TryRemove(response.CorrelationId, out _);
                }
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: "driver-identification-response", autoAck: false, consumer: consumer);
        }
        
        public async Task<DriverResponse> PublishAsync(long transporterId, long driverId)
        {
            var message = new DriverRequest
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Transporter_ID = transporterId,
                Driver_ID = driverId
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = message.CorrelationId;
            properties.ReplyTo = "driver-identification-response";
            _channel.BasicPublish(exchange: "", routingKey: "driver-identification-request", basicProperties: properties, body: body);
            var tcs = new TaskCompletionSource<DriverResponse>();
            _pendingRequests.TryAdd(message.CorrelationId, tcs);
            return await tcs.Task;
        }
    }
}