using Microsoft.EntityFrameworkCore;
using Shop.Core;
using Shop.Core.Entites;
using Shop.Core.IRepositories;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContex;

        public ProductRepository(OnlineShopDbContext onlineShopDbContex)
        {
            this.onlineShopDbContex = onlineShopDbContex;
        }
        public async Task<Product> Add(Product entity)
        {
            await onlineShopDbContex.AddAsync(entity);
            return entity;
        }

        public async Task<Product> Get(Guid Id)
        {
            return await onlineShopDbContex.Products.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await onlineShopDbContex.Products.ToListAsync();
        }
    }
}