using AutoMapper;
using MediatR;
using Shop.Application.Interfaces;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;

namespace Shop.Application.CQRS.ProductQueryCommand.Query
{
    public class ProductGetByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }

    public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, ProductDto>
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductGetByIdQueryHandler(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public async Task<ProductDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await service.GetById(request.Id);
        }
    }
}