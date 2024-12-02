using Product.Business.Models;

namespace Product.Business.Services.Interfaces;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryPayload>> GetProductCategories();

    Task<ProductCategoryPayload?> GetProductCategoryByName(string productCategoryName);

    Task<ProductCategoryPayload> AddProductCategory(string productCategoryName);

    Task<ProductCategoryPayload> UpdateProductCategory(int id, string productCategoryName);

    Task DeleteProductCategory(int id);
}
