using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Entities.OrderAggregate;

namespace CORE.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order>UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order>UpdateOrderPaymentFailed(string paymentIntentId);

    }
}