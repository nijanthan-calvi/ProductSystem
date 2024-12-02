using Microsoft.EntityFrameworkCore;
using Product.DataAccess.DataContext;
using Product.DataAccess.Entities;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.DataAccess.Repositories;

public class ProductCategoryRepository(ProductDataContext context) : IProductCategoryRepository
{
    private readonly ProductDataContext _context = context;

    public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
    {
        return await _context.ProductCategory.ToListAsync();
    }

    public async Task<ProductCategory?> GetCategoryByNameAsync(string categoryName)
    {
        return await _context.ProductCategory.FirstOrDefaultAsync(p => p.CategoryName == categoryName.ToLower());
    }

    public async Task<ProductCategory> AddProductCategoryAsync(ProductCategory productCategory)
    {
        _context.ProductCategory.Add(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }

    public async Task<ProductCategory?> UpdateProductCategoryAsync(int id, string productCategoryName)
    {
        var existingProductCategory = await _context.ProductCategory.FirstOrDefaultAsync(p => p.CategoryId == id);
        if (existingProductCategory == null) return null;

        existingProductCategory.CategoryName = productCategoryName;

        await _context.SaveChangesAsync();
        return existingProductCategory;
    }

    public async Task<bool> DeleteProductCategoryAsync(int id)
    {
        var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(p => p.CategoryId == id);
        if (productCategory == null) return false;

        _context.ProductCategory.Remove(productCategory);
        await _context.SaveChangesAsync();
        return true;
    }
}
