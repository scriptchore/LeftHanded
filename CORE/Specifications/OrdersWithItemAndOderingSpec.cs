using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE.Entities.OrderAggregate;

namespace CORE.Specifications
{
    public class OrdersWithItemAndOderingSpec : BaseSpecification<Order>
    {
        public OrdersWithItemAndOderingSpec(string email) : base(o => o.BuyerEmail == email) 
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDesc(o => o.OrderDate);
        }

        public OrdersWithItemAndOderingSpec(int id, string email) : base(o => o.BuyerEmail == email 
        && o.Id == id)
        {
            AddInclude(o => o.OrderDate);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}