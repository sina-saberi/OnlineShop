using Microsoft.AspNetCore.Http;

namespace Shop.Infrastructure.Dto
{
    public class ProductDto
    {
        public Guid? Id { get; set; }

        public string? ProductName { get; set; }

        public long Price { get; set; }

        public string? PriceWithComma { get; set; }
        public IFormFile file { get; set; }
        public string? FileBase64 { get; set; }
        public string? FilePath { get; set; }
    }
}