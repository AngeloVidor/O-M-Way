using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.API.DTOs
{
    public class TransporterAuthDTO
    {
        public long Transporter_ID { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public TransporterAuthDTO()
        {
            Role = "Transporter";
        }
    }
}