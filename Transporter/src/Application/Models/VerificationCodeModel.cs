using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Application.Models
{
    public class VerificationCodeModel
    {
        [Key]
        public long VerificationCode_ID { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }

    }
}