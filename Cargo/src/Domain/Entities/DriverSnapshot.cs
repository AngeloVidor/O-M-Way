using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Domain.Entities
{
    public class DriverSnapshot
    {
        [Key]
        public long Snapshot_ID { get; set; }
        public long Transporter_ID { get; set; }
        public long Employee_ID { get; set; }
        public string Username { get; set; }
    }
}