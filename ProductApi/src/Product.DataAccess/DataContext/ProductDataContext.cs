using Microsoft.EntityFrameworkCore;
using Product.DataAccess.Entities;
using Product.DataAccess.EntityMap;

namespace Product.DataAccess.DataContext;

public class ProductDataContext(DbContextOptions<ProductDataContext> options) : DbContext(options)
{
    public DbSet<Entities.Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ProductMap.Map(modelBuilder);
        ProductCategoryMap.Map(modelBuilder);
    }

}
