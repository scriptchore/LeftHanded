using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using CORE.Entities.OrderAggregate;
using CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this._mapper = mapper;
            this._orderService = orderService;
            
        }
        

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, 
            orderDto.BasketId,
            address);

            if(order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);

        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderToreturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.GetEmailFromPrincipal();

            var orders = await _orderService.GetOrderForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToreturnDto>>(orders));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<OrderToreturnDto>> GetOrdersByIdForUser(int id)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();

            var orders = await _orderService.GetOrderByIdAsync(id, email);

            if(orders == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToreturnDto>(orders);
        }


        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}