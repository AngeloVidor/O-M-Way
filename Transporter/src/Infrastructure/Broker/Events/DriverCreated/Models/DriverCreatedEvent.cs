using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Events.DriverCreated.Models
{
    public class DriverCreatedEvent
    {
        public string CorrelationId { get; set; }
        public long Transporter_ID { get; set; }
        public long Employee_ID { get; set; }
        public string Username { get; set; }
    }
}