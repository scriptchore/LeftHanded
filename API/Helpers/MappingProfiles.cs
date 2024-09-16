
using API.DTOs;
using AutoMapper;
using CORE.Entities;
using CORE.Entities.Identity;
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
             CreateMap<Address, AddressDto>().ReverseMap();
             CreateMap<CustomerBasketDto, CustomerBasket>();
             CreateMap<BasketItemDto, BasketItem>();
             CreateMap<AddressDto, CORE.Entities.OrderAggregate.Address>();
            
        }
    }
}