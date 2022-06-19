using AutoMapper;
using MediatR;
using Shop.Application.Interfaces;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;

namespace Shop.Application.CQRS.ProductQueryCommand.Query
{
    public class ProductGetAllQuery : IRequest<ICollection<ProductDto>>
    {

    }

    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, ICollection<ProductDto>>
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductGetAllQueryHandler(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public async Task<ICollection<ProductDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            return (await service.Getall()).ToList();
        }
    }
}