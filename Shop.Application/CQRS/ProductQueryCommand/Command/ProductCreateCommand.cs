using AutoMapper;
using MediatR;
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
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ProductCreateCommandHandler(
            IProductRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var NewProductEntity = mapper.Map<Product>(request.Data);
            if (request.Data.file != null)
            {
                var file = request.Data.file.SaveAndGetFileModel(FileUtility.PROJECT_ROOT + @$"\{request.Data.ProductName}");
                NewProductEntity.Thumbnail = file.File;
                NewProductEntity.ThumbnailFileName = file.FileName;
                NewProductEntity.ThumbnailFileSize = file.FileSize;
                NewProductEntity.ThumbnailFileExtension = file.FileExtension;
            }
            var entity = await repository.Add(NewProductEntity);
            await unitOfWork.SaveCahngesAsync();
            return entity.Id;
        }
    }
}