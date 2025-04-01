using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using src.Application.Models;

namespace src.Domain.Entities
{
    public class TransporterCompany
    {
        [Key]
        public long Transporter_ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public long VerificationCode_ID { get; set; }
        [ForeignKey("VerificationCode_ID")]
        public VerificationCodeModel VerificationCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}