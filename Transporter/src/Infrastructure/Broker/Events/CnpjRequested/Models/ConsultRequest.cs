using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Broker.Events.CnpjRequested.Models
{
    public class ConsultRequest
    {
        public string CorrelationId { get; set; }
        public long Transporter_ID { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public DateTime TimeAddedToList { get; set; }
    }

}