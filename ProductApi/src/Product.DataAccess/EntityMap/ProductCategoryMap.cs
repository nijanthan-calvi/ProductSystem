using Microsoft.EntityFrameworkCore;
using Product.DataAccess.Entities;

namespace Product.DataAccess.EntityMap;

public static class ProductCategoryMap
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("PRODUCT_CATEGORIES");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID").HasColumnType("int").ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).HasColumnName("CATEGORY_NAME").HasColumnType("nvarchar(50)");
            entity.HasKey(e => e.CategoryId);
        });
    }
}
