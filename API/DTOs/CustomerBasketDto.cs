using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Entities.Identity;

namespace API.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
         //[Required]
        public List<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
         public decimal ShippingPrice { get; set; }
    }
}