using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Driver.Interface
{
    public interface IDriverIdentificationPublisher
    {
        Task PublishAsync(long transporterId, long driverId);
    }
}