using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructure.Broker.Driver.Messages;

namespace src.Infrastructure.Broker.Driver.Interface
{
    public interface IDriverIdentificationPublisher
    {
        Task<DriverResponse> PublishAsync(long transporterId, long driverId);
    }
}