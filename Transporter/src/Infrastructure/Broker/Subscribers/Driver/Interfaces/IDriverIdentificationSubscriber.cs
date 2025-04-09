using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructure.Broker.Subscribers.Driver.Messages;

namespace src.Infrastructure.Broker.Subscribers.Driver.Interfaces
{
    public interface IDriverIdentificationSubscriber
    {
        void Consume();
        Task Publish(DriverResponse driverResponse, string replyTo);
    }
}