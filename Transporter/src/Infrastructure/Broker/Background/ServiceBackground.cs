using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.UseCases.Employees.Interfaces;
using src.Infrastructure.Broker.Subscribers.Driver.Interfaces;

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
            var driverConsumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDriverIdentificationSubscriber>();
            driverConsumer.Consume();
        }
    }
}