using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static src.Domain.Entities.Load;

namespace src.API.DTOs
{
    public class LoadDTO
    {
        [Key]
        public long Load_ID { get; set; }
        public long Transporter_ID { get; set; } //fk
        public long Driver_ID { get; set; } //fk
        public DateTime LoadingStartedAt { get; set; } = DateTime.Now;
        //public DateTime LoadingFinishedAt { get; set; }
        public DateTime DeliveryDeadline { get; set; }
        //public DateTime DeliveredAt { get; set; }
        public bool IsDelivered { get; set; } = false;
        public LoadStatus Status { get; set; }    
    }
}