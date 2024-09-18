using System;
using System.Collections.Generic;
using CORE.Entities.OrderAggregate;


namespace API.DTOs
{
    public class OrderToreturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set;}
        public DateTime OrderDate { get; set;}
        public Address ShipToAddress { get; set;}
        public string DeliveryMethod { get; set;}
        public decimal ShippingPrice { get; set;}

        public IReadOnlyList<OrderItemDto> OrderItems { get; set;}
        public decimal SubTotal { get; set;}
        public decimal Total { get; set;}

        public string Status { get; set;} 

       
    }
}