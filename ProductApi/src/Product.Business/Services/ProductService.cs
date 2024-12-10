using AutoMapper;
using Product.Business.Exceptions;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Product.DataAccess.Entities;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.Business.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductPayload>> GetAllProducts(string? search, string? sort, string? direction)
    {
        var result = await _productRepository.GetAllProductsAsync();
        var response = _mapper.Map<IEnumerable<ProductPayload>>(result);

        // Filter by search term
        if (!string.IsNullOrEmpty(search))
        {
            response = response.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        // Sort by column
        if (!string.IsNullOrEmpty(sort))
        {
            response = sort.ToLower() switch
            {
                "name" => direction == "asc"
                    ? response.OrderBy(p => p.Name)
                    : response.OrderByDescending(p => p.Name),
                "description" => direction == "asc"
                    ? response.OrderBy(p => p.Description)
                    : response.OrderByDescending(_ => _.Description),
                "category" => direction == "asc"
                    ? response.OrderBy(p => p.Category)
                    : response.OrderByDescending(p => p.Category),
                "price" => direction == "asc"
                    ? response.OrderBy(p => p.Price)
                    : response.OrderByDescending(p => p.Price),
                _ => response
            };
        }

        return response;
    }

    public async Task<ProductPayload> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product == null ? throw new NoDataFoundException($"No product is found with id: {id}") : _mapper.Map<ProductPayload>(product);
    }

    public async Task<ProductPayload> AddProduct(ProductPayload product)
    {
        //Check if product already exists
        var exisitingProduct = await _productRepository.GetProductByNameAsync(product.Name);
        if (exisitingProduct != null)
        {
            throw new DuplicateDataException($"Already Product is exists with product name: {product.Name}");
        }

        // Check if the category already exists
        var existingCategory = await _productRepository.GetCategoryByNameAsync(product.Category);
        int categoryId;
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

    public async Task<ProductPayload> UpdateProduct(int id, ProductPayload product)
    {
        //Check if product already exists
        _ = await _productRepository.GetProductByIdAsync(id)
            ?? throw new NoDataFoundException($"No product found with this product id: {id}");
        var productDetails = _mapper.Map<DataAccess.Entities.Product>(product);
        await _productRepository.UpdateProductAsync(id, productDetails);
        return product;
    }

    public async Task DeleteProductById(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}
