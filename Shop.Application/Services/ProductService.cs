using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Core.IRepositories;
using Shop.Infrastructure.Dto;
using Shop.Infrastructure.Interfaces;
using Shop.Infrastructure.Model;
using Shop.Infrastructure.Utility;

namespace Shop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly FileUtility fileUtility;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ProductService> logger;

        public ProductService(
            IMapper mapper,
            FileUtility fileUtility,
            IProductRepository repository,
            IUnitOfWork unitOfWork,
            ILogger<ProductService> logger)
        {
            this.mapper = mapper;
            this.fileUtility = fileUtility;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<ProductDto> Add(ProductDto dto)
        {
            logger.LogInformation("Call Add from ProductService");
            var product = mapper.Map<Product>(dto);
            product.Thumbnail = fileUtility.ConvertToByteArray(dto.file);
            product.ThumbnailFileExtension = fileUtility.GetfileExtension(dto.file.FileName);
            product.ThumbnailFileName = fileUtility.SaveFileInFolder<Product>(dto.file);
            product.ThumbnailFileSize = dto.file.Length;

            await repository.Add(product);
            await unitOfWork.SaveCahngesAsync();

            return mapper.Map<ProductDto>(product);
        }

        public async Task<ShopActionResult<IEnumerable<ProductDto>>> Getall(int page = 1, int size = 10)
        {
            var products = repository.GetAll();
            logger.LogInformation("call get all from productservice");
            return new()
            {
                Page = page,
                Size = size,
                Total = await products.CountAsync(),
                Data = mapper.Map<IEnumerable<ProductDto>>(
                 await products
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync()),
            };
        }

        public async Task<ProductDto> GetById(Guid Id)
        {
            logger.LogInformation("call getById from productservice");
            //TODO:use auto mapper
            var entity = await repository.Get(Id);
            return new ProductDto()
            {
                Id = entity.Id,
                FileBase64 = fileUtility.ConvertToBase64(entity.Thumbnail),
                FilePath = fileUtility.GetFileUrl<Product>(entity.ThumbnailFileName),
                Price = entity.Price,
                ProductName = entity.ProductName,
                PriceWithComma = entity.Price.ToString("#,##0")
            };
        }
    }
}