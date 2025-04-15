using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Events.Subscriber.DriverCreated.Interfaces
{
    public interface IDriverEventConsumer
    {
        void Consume();
    }
}