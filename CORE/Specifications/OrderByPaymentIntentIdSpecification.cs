using System.Linq.Expressions;
using CORE.Entities.OrderAggregate;

namespace CORE.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) 
        : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}