using System.IO;
using System.Threading.Tasks;
using API.Errors;
using CORE.Entities;
using CORE.Entities.OrderAggregate;
using CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private const string WhSecret = "whsec_75c3280e1bff804edd2628f7b667a5c5360627ca353f6f2f34ef17a5c738b72a";
        private readonly IPaymentService _PaymentService;
        public ILogger<PaymentController> _logger { get; }

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _PaymentService = paymentService;    
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _PaymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ApiResponse(400, "Unable to update basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
             Request.Headers["Stripe-Signature"], WhSecret);

             PaymentIntent intent;
             Order order;

             switch (stripeEvent.Type)
             {
                case "payment_intent.succeeded" :
                intent = (PaymentIntent) stripeEvent.Data.Object;
                _logger.LogInformation("payment succeeded: ", intent.Id);


                order = await _PaymentService.UpdateOrderPaymentSucceeded(intent.Id);
                _logger.LogInformation("Order Updated to received", order.Id);
                break;

                 case "payment_intent.payment_failed" :
                intent = (PaymentIntent) stripeEvent.Data.Object;
                _logger.LogInformation("payment succeeded: ", intent.Id);
                 order = await _PaymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Order Updated to failed", order.Id);
                break;

               
             }

              return new EmptyResult();
        }
        
    }
}