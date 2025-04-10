using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Driver.Messages
{
    public class DriverResponse
    {
        public string CorrelationId { get; set; }
        public long Transporter_ID { get; set; }
        public string Username { get; set; }
        public long Driver_ID { get; set; }
        public string ErrorMessage { get; set; }
    }
}