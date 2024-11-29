using System.ComponentModel.DataAnnotations;

namespace Product.DataAccess.Entities;

public class ProductCategory
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
}
