using AutoMapper;
using Shop.Application.Interfaces;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;
using Shop.Infrastructure.Interfaces;
using Shop.Infrastructure.Utility;

namespace Shop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly FileUtility fileUtility;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(
            IMapper mapper,
            FileUtility fileUtility,
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.fileUtility = fileUtility;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Add(ProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            product.Thumbnail = fileUtility.ConvertToByteArray(dto.file);
            product.ThumbnailFileExtension = fileUtility.GetfileExtension(dto.file.FileName);
            product.ThumbnailFileName = fileUtility.SaveFileInFolder(dto.file, product);
            product.ThumbnailFileSize = dto.file.Length;

            await repository.Add(product);
            await unitOfWork.SaveCahngesAsync();

            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> Getall()
        {
            return mapper.Map<IEnumerable<ProductDto>>(await repository.GetAll());
        }

        public async Task<ProductDto> GetById(Guid Id)
        {
            //TODO:use auto mapper
            var entity = await repository.Get(Id);
            return new ProductDto()
            {
                Id = entity.Id,
                FileBase64 = fileUtility.ConvertToBase64(entity.Thumbnail),
                FilePath = fileUtility.GetFileUrl(entity.ThumbnailFileName, entity),
                Price = entity.Price,
                ProductName = entity.ProductName,
                PriceWithComma = entity.Price.ToString("#,##0")
            };
        }
    }
}