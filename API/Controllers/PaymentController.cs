using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _PaymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _PaymentService = paymentService;    
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            return await _PaymentService.CreateOrUpdatePaymentIntent(basketId);
        }
        
    }
}