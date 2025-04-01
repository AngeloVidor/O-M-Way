using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Domain.Entities
{
    public class Location
    {
        [Key]
        public long Location_ID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string CEP { get; set; }
        public DateTime Timestamp { get; set; }
        public long Transporter_ID { get; set; }
    }
}