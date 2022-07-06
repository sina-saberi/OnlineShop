using Shop.Core.Entites;

namespace Shop.Core.IRepositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        Task<Product> Get(Guid Id);
        Task<Product> Add(Product entity);
    }
}