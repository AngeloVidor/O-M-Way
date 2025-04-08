using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Domain.Entities
{
    public class Employee
    {
        [Key]
        public long Employee_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Transporter_ID { get; set; }
        [ForeignKey("Transporter_ID")]
        public TransporterCompany TransporterCompany { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}