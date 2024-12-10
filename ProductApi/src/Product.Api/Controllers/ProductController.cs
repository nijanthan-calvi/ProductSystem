using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Business.Exceptions;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController
    (
    IProductService productService,
    ILogger<ProductController> logger
    ) : Controller
{
    private readonly IProductService _productService = productService;

    /// <summary>
    /// Get a list of product details.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a collection of products.
    /// </remarks>
    /// <param name="search"></param>
    /// <param name="sort"></param>
    /// <param name="direction"></param>
    /// <response code="200">Returns a list of products</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    /// <summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<ProductPayload>), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductPayload>>> GetAllProducts(
        string? search = null, string? sort = null, string? direction = "asc")
    {
        try
        {
            var products = await _productService.GetAllProducts(search, sort, direction);

            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get a information of a product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a detail of the product.
    /// </remarks>
    /// <param name="id"></param>
    /// <response code="200">Returns detail of a product</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<ProductPayload>> GetProductById([FromRoute] int id)
    {
        try
        {
            var product = await _productService.GetProductById(id);

            return Ok(product);
        }
        catch (NoDataFoundException ex)
        {
            logger.LogError(ex, "An handled exception occured");
            return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Create a Product
    /// </summary>
    /// <remarks>
    /// This endpoint will create a product.
    /// </remarks>
    /// <param name="product"></param>
    /// <response code="201">Returns created product</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [Authorize]
    [Produces("application/json")]
    public async Task<ActionResult<ProductPayload>> AddProduct([FromBody] ProductPayload product)
    {
        try
        {
            var response = await _productService.AddProduct(product);

            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch (DuplicateDataException ex)
        {
            logger.LogError(ex, "An handled exception occured");
            return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update a product.
    /// </summary>
    /// <remarks>
    /// This endpoint updates the product.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <response code="200">Returns 200 Object</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPut]
    [Authorize]
    [Route("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateProduct([FromRoute, Required] int id, [FromBody] ProductPayload product)
    {
        try
        {
            await _productService.UpdateProduct(id, product);
            return Ok();
        }
        catch (NoDataFoundException ex)
        {
            logger.LogError(ex, "An handled exception occured");
            return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete a product.
    /// </summary>
    /// <remarks>
    /// This endpoint will delete the product with product id.
    /// </remarks>
    /// <param name="id"></param>
    /// <response code="204">Returns No Content</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpDelete]
    [Authorize]
    [Route("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteProductById([FromRoute, Required] int id)
    {
        try
        {
            await _productService.DeleteProductById(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
