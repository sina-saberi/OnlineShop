using AutoMapper;
using MediatR;
using Shop.Application.Interfaces;
using Shop.Application.Services;
using Shop.Core.Entites;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;
using Shop.Infrastructure.Interfaces;
using Shop.Infrastructure.Utility;

namespace Shop.Application.CQRS.ProductQueryCommand.Command
{
    public class ProductCreateCommand : IRequest<Guid>
    {
        public ProductDto Data { get; set; } = new();
    }

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IProductService service;

        public ProductCreateCommandHandler(
            IMapper mapper,
            IProductService service
            )
        {
            this.mapper = mapper;
            this.service = service;
        }
        public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            return (await service.Add(request.Data)).Id!.Value;
        }
    }
}