using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Entites;
public class Product
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(128), Required]
    public string? ProductName { get; set; }
    public long Price { get; set; }
    public byte[] Thumbnail { get; set; }
    public string ThumbnailFileName { get; set; }
    public long ThumbnailFileSize { get; set; }
    public string ThumbnailFileExtension { get; set; }
}