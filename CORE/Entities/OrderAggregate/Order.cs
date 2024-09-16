using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shipToAddress, 
        DeliveryMethod deliveryMethod, decimal subTotal) 
        {
            this.BuyerEmail = buyerEmail;
            this.ShipToAddress = shipToAddress;
            this.DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            this.SubTotal = subTotal;
            
   
        }
        public string BuyerEmail { get; set;}
        public DateTime OrderDate { get; set;} = DateTime.UtcNow;
        public Address ShipToAddress { get; set;}
        public DeliveryMethod DeliveryMethod { get; set;}
        public IReadOnlyList<OrderItem> OrderItems { get; set;}
        public decimal SubTotal { get; set;}
        public OrderStatus Status { get; set;} = OrderStatus.Pending;
        public string PaymentIntentId { get; set;}

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }







        
    }
}