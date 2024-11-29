using System.ComponentModel.DataAnnotations;

namespace Product.DataAccess.Entities;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductDescription { get; set; }
    public int CategoryId { get; set; }
    public decimal ProductPrice { get; set; }
    public ProductCategory Category { get; set; }
}
