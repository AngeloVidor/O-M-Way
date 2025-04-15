using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.UseCases.DriverSnapshots.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Models;
using src.Infrastructure.Repositories.Interfaces.DriverSnapshots;

namespace src.Application.UseCases.DriverSnapshots.Implementations
{
    public class DriverSnapshotService : IDriverSnapshotService
    {
        private readonly IDriverSnapshotRepository _driverSnapshotRepository;

        public DriverSnapshotService(IDriverSnapshotRepository driverSnapshotRepository)
        {
            _driverSnapshotRepository = driverSnapshotRepository;
        }

        public async Task<bool> SaveCopyAsync(DriverCreatedEvent @event)
        {
            var snapshot = new DriverSnapshot
            {
                Transporter_ID = @event.Transporter_ID,
                Employee_ID = @event.Employee_ID,
                Username = @event.Username
            };
            return await _driverSnapshotRepository.SaveCopyAsync(snapshot);
        }
    }
}