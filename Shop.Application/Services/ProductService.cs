using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Infrastructure.Dto;

namespace Shop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly OnlineShopDbContext contex;
        private readonly IMapper mapper;

        public ProductService(OnlineShopDbContext contex, IMapper mapper)
        {
            this.contex = contex;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Add(ProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            await contex.AddAsync(product);
            await contex.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> Getall()
        {
            return mapper.Map<IEnumerable<ProductDto>>(await contex.Products.ToListAsync());
        }

        public async Task<ProductDto> GetById(Guid Id)
        {
            return mapper.Map<ProductDto>(await contex.Products.FirstOrDefaultAsync(p => p.Id == Id));
        }
    }
}