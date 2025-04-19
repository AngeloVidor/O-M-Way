using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Domain.Entities
{
    public class PendingRegistration
    {
        [Key]
        public long PendingTransporter_ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public PendingLocation Location { get; set; }
        public string VerificationCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCnpj_Valid { get; set; } = false;
        public bool Cnpj_Validated { get; set; }
    }
}