using Shop.Core.Entites;
using Shop.Infrastructure.Dto;

namespace Shop.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> Getall();

        Task<ProductDto> GetById(Guid Id);

        Task<ProductDto> Add(ProductDto dto);
    }
}