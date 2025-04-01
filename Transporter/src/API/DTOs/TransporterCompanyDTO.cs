using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using src.Application.Models;
using src.Domain.Entities;

namespace src.API.DTOs
{
    public class TransporterCompanyDTO
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
        public DateTime CreatedAt { get; set; }
    }
}