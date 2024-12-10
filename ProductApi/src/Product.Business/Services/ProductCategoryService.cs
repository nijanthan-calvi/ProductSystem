using AutoMapper;
using Product.Business.Exceptions;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Product.DataAccess.Entities;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.Business.Services;

public class ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper) : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductCategoryPayload>> GetProductCategories()
    {
        var result = await _productCategoryRepository.GetAllProductCategoriesAsync();
        var response = _mapper.Map<IEnumerable<ProductCategoryPayload>>(result);
        return response;
    }

    public async Task<ProductCategoryPayload?> GetProductCategoryByName(string productCategoryName)
    {
        var product = await _productCategoryRepository.GetCategoryByNameAsync(productCategoryName);

        return product == null
            ? throw new NoDataFoundException($"No product category is found with name: {productCategoryName}")
            : _mapper.Map<ProductCategoryPayload>(product);
    }

    public async Task<ProductCategoryPayload> AddProductCategory(string productCategoryName)
    {
        // Check if the category already exists
        var existingCategory = await _productCategoryRepository.GetCategoryByNameAsync(productCategoryName);

        if (existingCategory != null)
        {
            throw new DuplicateDataException($"Already Product Category is exists with name: {productCategoryName}");
        }

        // Map the product payload to the entity and set the category ID
        var productCategoryDetails = new ProductCategory
        {
            CategoryName = productCategoryName
        };

        // Save the product
        var response = await _productCategoryRepository.AddProductCategoryAsync(productCategoryDetails);

        // Map the saved entity back to the payload and return
        return _mapper.Map<ProductCategoryPayload>(response);
    }

    public async Task<ProductCategoryPayload> UpdateProductCategory(int id, string productCategoryName)
    {
        //Check if product already exists
        _ = await _productCategoryRepository.GetCategoryByNameAsync(productCategoryName)
            ?? throw new NoDataFoundException($"No product category found with this name: {productCategoryName}");
        var response = await _productCategoryRepository.UpdateProductCategoryAsync(id, productCategoryName);

        return _mapper.Map<ProductCategoryPayload>(response);
    }

    public async Task DeleteProductCategory(int id)
    {
        await _productCategoryRepository.DeleteProductCategoryAsync(id);
    }
}
