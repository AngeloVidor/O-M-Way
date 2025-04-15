using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Interfaces;

namespace src.Infrastructure.Broker.Background
{
    public class ServiceBackground : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceBackground(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var driverConsumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDriverEventConsumer>();
            driverConsumer.Consume();
        }

    }
}