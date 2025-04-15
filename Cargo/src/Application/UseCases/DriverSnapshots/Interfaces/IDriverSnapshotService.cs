using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Models;

namespace src.Application.UseCases.DriverSnapshots.Interfaces
{
    public interface IDriverSnapshotService
    {
        Task<bool> SaveCopyAsync(DriverCreatedEvent @event);

    }
}