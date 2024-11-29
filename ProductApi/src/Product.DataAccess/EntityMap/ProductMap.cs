using Microsoft.EntityFrameworkCore;

namespace Product.DataAccess.EntityMap;

public static class ProductMap
{
    public static void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Product>(entity =>
        {
            entity.ToTable("PRODUCTS");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID").HasColumnType("int").ValueGeneratedOnAdd();
            entity.Property(e => e.ProductName).HasColumnName("PRODUCT_NAME").HasColumnType("nvarchar(100)");
            entity.Property(e => e.ProductDescription).HasColumnName("PRODUCT_DESCRIPTION").HasColumnType("nvarchar(max)");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID").HasColumnType("int");
            entity.Property(e => e.ProductPrice).HasColumnName("PRODUCT_PRICE").HasColumnType("decimal(18, 2)");
            entity.HasKey(e => e.ProductId);
        });
    }
}
