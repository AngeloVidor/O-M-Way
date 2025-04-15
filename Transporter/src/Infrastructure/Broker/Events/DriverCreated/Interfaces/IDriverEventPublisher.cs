using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Events.DriverCreated.Interfaces
{
    public interface IDriverEventPublisher
    {
        Task Publish(long transporterId, long employeeId, string username);
    }
}