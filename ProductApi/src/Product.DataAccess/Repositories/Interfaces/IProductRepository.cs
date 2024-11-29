namespace Product.DataAccess.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
    Task<Entities.Product?> GetProductByIdAsync(int id);
    Task<Entities.Product> AddProductAsync(Entities.Product product);
    Task<Entities.Product?> UpdateProductAsync(int id, Entities.Product product);
    Task<bool> DeleteProductAsync(int id);
}
