using AutoMapper;
using Shop.Application.CQRS.Notfication;
using Shop.Core.Entites;
using Shop.Core.Entites.Security;
using Shop.Infrastructure.Dto;

namespace Shop.Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //product
            CreateMap<ProductDto, Product>()
                .ReverseMap();
            CreateMap<Product, ProductDto>()
            .ForMember(m => m.PriceWithComma, option => option.MapFrom(m => m.Price.ToString("#,##0")))
            .ReverseMap();

            //user
            CreateMap<UserRefreshToken, AddRefresTokenNotfication>().ReverseMap();
        }
    }
}