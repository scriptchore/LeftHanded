
using API.DTOs;
using AutoMapper;
using CORE.Entities;
using CORE.Entities.Identity;
using CORE.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, ProductDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
             .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
             CreateMap<CORE.Entities.Identity.Address, AddressDto>().ReverseMap();
             CreateMap<CustomerBasketDto, CustomerBasket>();
             CreateMap<BasketItemDto, BasketItem>();
             CreateMap<AddressDto, CORE.Entities.OrderAggregate.Address>();
             CreateMap<Order, OrderToreturnDto>()
             .ForMember(d => d.DeliveryMethod, o  => o.MapFrom(s => s.DeliveryMethod.ShortName))
             .ForMember(d => d.ShippingPrice, o  => o.MapFrom(s => s.DeliveryMethod.Price));

             CreateMap<OrderItem, OrderItemDto>()
             .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
             .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
             .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlresolver>());




            
        }
    }
}