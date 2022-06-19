
using Shop.Core;
using Shop.Infrastructure.Interfaces;

namespace Shop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext context;

        public UnitOfWork(OnlineShopDbContext context)
        {
            this.context = context;
        }
        public async Task<int> SaveCahngesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}