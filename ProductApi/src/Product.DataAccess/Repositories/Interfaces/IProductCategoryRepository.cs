using Product.DataAccess.Entities;

namespace Product.DataAccess.Repositories.Interfaces;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
    Task<ProductCategory?> GetCategoryByNameAsync(string categoryName);
    Task<ProductCategory> AddProductCategoryAsync(ProductCategory productCategory);
    Task<ProductCategory?> UpdateProductCategoryAsync(int id, string productCategoryName);
    Task<bool> DeleteProductCategoryAsync(int id);
}
