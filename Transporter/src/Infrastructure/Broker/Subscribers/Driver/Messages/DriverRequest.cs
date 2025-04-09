using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Subscribers.Driver.Messages
{
    public class DriverRequest
    {

        public string CorrelationId { get; set; }
        public long Transporter_ID { get; set; }
        public long Driver_ID { get; set; }
    }
}