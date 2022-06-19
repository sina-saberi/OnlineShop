using AutoMapper;
using MediatR;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;

namespace Shop.Application.CQRS.ProductQueryCommand.Query
{
    public class ProductGetAllQuery : IRequest<ICollection<ProductDto>>
    {

    }

    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, ICollection<ProductDto>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductGetAllQueryHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ICollection<ProductDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var res = await repository.GetAll();
            return mapper.Map<ICollection<ProductDto>>(res);
        }
    }
}