using AutoMapper;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Product.DataAccess.Entities;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductPayload>> GetProducts()
    {
        var result = await _productRepository.GetAllProductsAsync();
        var response = _mapper.Map<IEnumerable<ProductPayload>>(result);
        return response;
    }

    public async Task<ProductPayload> GetProduct(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return null;

        return _mapper.Map<ProductPayload>(product);
    }

    public async Task<ProductPayload> PostProduct(ProductPayload product)
    {
        // Check if the category already exists
        var existingCategory = await _productRepository.GetCategoryByNameAsync(product.Category);
        int categoryId = 0;

        if (existingCategory != null)
        {
            // Use the existing category ID
            categoryId = existingCategory.CategoryId;
        }
        else
        {
            // Create a new category
            var newCategory = new ProductCategory
            {
                CategoryName = product.Category
            };

            var newProductCategory = _productRepository.AddProductCategoryAsync(newCategory);
            categoryId = newProductCategory.Result.CategoryId;
        }

        // Map the product payload to the entity and set the category ID
        var productDetails = _mapper.Map<DataAccess.Entities.Product>(product);
        productDetails.CategoryId = categoryId;
        productDetails.Category = null;

        // Save the product
        var response = await _productRepository.AddProductAsync(productDetails);

        // Map the saved entity back to the payload and return
        return _mapper.Map<ProductPayload>(response);
    }

    public async Task<ProductPayload> PutProduct(int id, ProductPayload product)
    {
        var productDetails = _mapper.Map<DataAccess.Entities.Product>(product);
        await _productRepository.UpdateProductAsync(id, productDetails);
        return product;
    }

    public async Task DeleteProduct(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}
