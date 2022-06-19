using Shop.Core.Entites;

namespace Shop.Core.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(Guid Id);
        Task<Product> Add(Product entity);
    }
}