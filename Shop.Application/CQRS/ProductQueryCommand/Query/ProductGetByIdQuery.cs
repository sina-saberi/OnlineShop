using AutoMapper;
using MediatR;
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
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductGetByIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProductDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<ProductDto>(await repository.Get(request.Id));
        }
    }
}