using Microsoft.EntityFrameworkCore;
using Product.DataAccess.DataContext;
using Product.DataAccess.Entities;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.DataAccess.Repositories;

public class ProductRepository(ProductDataContext context) : IProductRepository
{
    private readonly ProductDataContext _context = context;

    public async Task<IEnumerable<Entities.Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Entities.Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<ProductCategory?> GetCategoryByNameAsync(string categoryName)
    {
        return await _context.ProductCategory.FirstOrDefaultAsync(p => p.CategoryName == categoryName.ToLower());
    }

    public async Task<Entities.Product> AddProductAsync(Entities.Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<ProductCategory> AddProductCategoryAsync(ProductCategory productCategory)
    {
        _context.ProductCategory.Add(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }

    public async Task<Entities.Product?> UpdateProductAsync(int id, Entities.Product product)
    {
        var existingProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        if (existingProduct == null) return null;

        existingProduct.ProductName = product.ProductName;
        existingProduct.ProductDescription = product.ProductDescription;
        existingProduct.ProductPrice = product.ProductPrice;

        if (!string.Equals(existingProduct.Category.CategoryName, product.Category.CategoryName, StringComparison.OrdinalIgnoreCase))
        {
            var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(p => p.CategoryName == product.Category.CategoryName.ToLower());
            existingProduct.Category = productCategory;
            existingProduct.CategoryId = productCategory.CategoryId;
        }

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
