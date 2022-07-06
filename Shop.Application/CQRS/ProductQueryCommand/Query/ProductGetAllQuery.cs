using AutoMapper;
using MediatR;
using Shop.Application.Interfaces;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;
using Shop.Infrastructure.Model;

namespace Shop.Application.CQRS.ProductQueryCommand.Query
{
    public class ProductGetAllQuery : IRequest<ShopActionResult<IEnumerable<ProductDto>>>
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
    }

    public class ProductGetAllQueryHandler : 
        IRequestHandler<ProductGetAllQuery, ShopActionResult<IEnumerable<ProductDto>>>
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductGetAllQueryHandler(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public async Task<ShopActionResult<IEnumerable<ProductDto>>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await service.Getall(request.page, request.size);
            return result;
        }
    }
}