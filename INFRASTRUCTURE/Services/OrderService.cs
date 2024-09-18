using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Entities.OrderAggregate;
using CORE.Interfaces;
using CORE.Specifications;

namespace INFRASTRUCTURE.Services
{
    public class OrderService : IOrderService
    {

        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
      



        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {

            _basketRepo = basketRepo;
            this._unitOfWork = unitOfWork;

            
            
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Products>().GetByIdAsync(item.Id);
                var itemOdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            _unitOfWork.Repository<Order>().Add(order);

            //save to db
        

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            //delete basket

            await _basketRepo.DeleteBasketAsync(basketId);

           
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
             var spec = new OrdersWithItemAndOderingSpec(id, buyerEmail);
             return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
           var spec = new OrdersWithItemAndOderingSpec(buyerEmail);

           return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}