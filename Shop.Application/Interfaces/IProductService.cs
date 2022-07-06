using Shop.Core.Entites;
using Shop.Infrastructure.Dto;
using Shop.Infrastructure.Model;

namespace Shop.Application.Interfaces
{
    public interface IProductService
    {
        Task<ShopActionResult<IEnumerable<ProductDto>>> Getall(int page = 1, int size = 10);

        Task<ProductDto> GetById(Guid Id);

        Task<ProductDto> Add(ProductDto dto);
    }
}