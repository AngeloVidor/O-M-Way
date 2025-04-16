using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;
using static src.Domain.Entities.Product;

namespace src.API.DTOs
{
    public class ProductDTO
    {
        [Key]
        public long Product_ID { get; set; }
        public long Load_ID { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string RecipientCPF { get; set; }
        public DeliveryStatus deliveryStatus { get; set; }

    }
}