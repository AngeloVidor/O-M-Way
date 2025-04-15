using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Domain.Entities
{
    public class Product
    {
        [Key]
        public long Product_ID { get; set; }
        public long Load_ID { get; set; }
        public long Address_ID { get; set; }
        [ForeignKey("Address_ID")]
        public Address Address { get; set; }
        public string Name { get; set; }
        public string RecipientCPF { get; set; }
        
        [ForeignKey("Load_ID")]
        public Load Load { get; set; }

        public enum DeliveryStatus
        {
            Created,
            EnRouteToRecipient,
            Delivered,
            DeliveryFailed,
            Returned,
            Cancelled
        }
    }
}