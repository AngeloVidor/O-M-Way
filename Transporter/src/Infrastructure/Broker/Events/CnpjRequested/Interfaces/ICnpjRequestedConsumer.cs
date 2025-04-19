using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Events.CnpjRequested.Interfaces
{
    public interface ICnpjRequestedConsumer
    {
        Task Consume();
    }
}