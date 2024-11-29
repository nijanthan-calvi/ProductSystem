using Product.Business.Models;

namespace Product.Business.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductPayload>> GetProducts();

    Task<ProductPayload> GetProduct(int id);

    Task<ProductPayload> PostProduct(ProductPayload product);

    Task<ProductPayload> PutProduct(int id, ProductPayload product);

    Task DeleteProduct(int id);
}
