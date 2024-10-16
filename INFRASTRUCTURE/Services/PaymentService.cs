using System.Linq;
using CORE.Entities;
using CORE.Entities.OrderAggregate;
using CORE.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace INFRASTRUCTURE.Services
{

    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                .GetByIdAsync((int)basket.DeliveryMethodId);
            }

            foreach(var item in basket.Items)
            {
                var productItems = await _unitOfWork.Repository<Products>().GetByIdAsync(item.Id);
                if(item.Price != productItems.Price)
                {
                    item.Price = productItems.Price;
                }
            }

            var Service = new PaymentIntentService();

            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100))
                     + (long) shippingPrice * 100,
                     Currency = "usd",
                     PaymentMethodTypes = new List<string> {"card"}

                };
                intent = await Service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                     Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100))
                     + (long) shippingPrice * 100
                };
                await Service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;


        }
    }
}