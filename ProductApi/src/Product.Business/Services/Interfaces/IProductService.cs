using Product.Business.Models;

namespace Product.Business.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductPayload>> GetAllProducts(string? search, string? sort, string? direction);

    Task<ProductPayload> GetProductById(int id);

    Task<ProductPayload> AddProduct(ProductPayload product);

    Task<ProductPayload> UpdateProduct(int id, ProductPayload product);

    Task DeleteProductById(int id);
}
