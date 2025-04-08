using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.API.DTOs
{
    public class EmployeeDTO
    {
        [Key]
        public long Employee_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Transporter_ID { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}