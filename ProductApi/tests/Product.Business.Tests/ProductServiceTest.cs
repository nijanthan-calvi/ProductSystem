using AutoMapper;
using NSubstitute;
using Product.Business.Models;
using Product.Business.Services;
using Product.Business.Services.Interfaces;
using Product.DataAccess.Repositories.Interfaces;
using Shouldly;

namespace Product.Business.Tests;

public class ProductServiceTest
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductServiceTest()
    {
        // Arrange the mock objects
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();

        // Create ProductService instance
        _productService = new ProductService(_productRepository, _mapper);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts_WhenNoFiltersAreApplied()
    {
        // Arrange
        SampleData(out List<DataAccess.Entities.Product> products, out List<ProductPayload> productPayloads);

        // Mock the repository and mapper
        _productRepository.GetAllProductsAsync().Returns(Task.FromResult(products.AsEnumerable()));
        _mapper.Map<IEnumerable<ProductPayload>>(products).Returns(productPayloads);

        // Act
        var result = await _productService.GetAllProducts(null, null, null);

        // Assert
        result.ShouldBeEquivalentTo(productPayloads);
        result.Count().ShouldBe(products.Count);
    }

    [Fact]
    public async Task GetAllProducts_ShouldFilterProducts_WhenSearchTermIsProvided()
    {
        // Arrange
        SampleData(out List<DataAccess.Entities.Product> products, out List<ProductPayload> productPayloads);

        // Mock the repository and mapper
        _productRepository.GetAllProductsAsync().Returns(Task.FromResult(products.AsEnumerable()));
        _mapper.Map<IEnumerable<ProductPayload>>(products).Returns(productPayloads);

        // Act
        var result = await _productService.GetAllProducts("Product A", null, null);

        // Assert
        result.Count().ShouldBe(1);
        result.First().Name.ShouldBe("Product A");
    }

    [Fact]
    public async Task GetAllProducts_ShouldSortProductsByPriceAscending_WhenSortByPriceAndAscDirection()
    {
        // Arrange
        SampleData(out List<DataAccess.Entities.Product> products, out List<ProductPayload> productPayloads);

        // Mock the repository and mapper
        _productRepository.GetAllProductsAsync().Returns(Task.FromResult(products.AsEnumerable()));
        _mapper.Map<IEnumerable<ProductPayload>>(products).Returns(productPayloads);

        // Act
        var result = await _productService.GetAllProducts(null, "price", "asc");

        // Assert
        result.First().Price.ShouldBe(10);
        result.Last().Price.ShouldBe(20);
    }

    [Fact]
    public async Task GetAllProducts_ShouldSortProductsByNameDescending_WhenSortByNameAndDescDirection()
    {
        // Arrange
        SampleData(out List<DataAccess.Entities.Product> products, out List<ProductPayload> productPayloads);

        // Mock the repository and mapper
        _productRepository.GetAllProductsAsync().Returns(Task.FromResult(products.AsEnumerable()));
        _mapper.Map<IEnumerable<ProductPayload>>(products).Returns(productPayloads);

        // Act
        var result = await _productService.GetAllProducts(null, "name", "desc");

        // Assert
        result.First().Name.ShouldBe("Product B");
        result.Last().Name.ShouldBe("Product A");
    }

    private static void SampleData(out List<DataAccess.Entities.Product> products, out List<ProductPayload> productPayloads)
    {
        products =
            [
                new() { ProductName = "Product A", ProductDescription = "Desc A", CategoryId = 1, ProductPrice = 10 },
                new() { ProductName = "Product B", ProductDescription = "Desc B", CategoryId = 2, ProductPrice = 20 }
            ];
        productPayloads =
            [
                new() { Name = "Product A", Description = "Desc A", Category = "Category A", Price = 10 },
                new() { Name = "Product B", Description = "Desc B", Category = "Category B", Price = 20 }
            ];
    }
}
