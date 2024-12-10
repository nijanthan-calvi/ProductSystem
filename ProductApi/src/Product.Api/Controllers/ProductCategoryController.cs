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
public class ProductCategoryController
    (
    IProductCategoryService productCategoryService,
    ILogger<ProductCategoryController> logger
    ) : Controller
{
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    /// <summary>
    /// Get a list of product category details.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a collection of product categories.
    /// </remarks>
    /// <response code="200">Returns a list of product categories</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductPayload>>> GetProductCategories()
    {
        try
        {
            var products = await _productCategoryService.GetProductCategories();

            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get detail of the product category.
    /// </summary>
    /// <remarks>
    /// This endpoint returns detail of the product category.
    /// </remarks>
    /// <param name="name"></param>
    /// <response code="200">Returns detail of the product category</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpGet]
    [Authorize]
    [Route("{name}")]
    [Produces("application/json")]
    public async Task<ActionResult<ProductCategoryPayload>> GetProductCategoryByName([FromRoute] string name)
    {
        try
        {
            var productCategory = await _productCategoryService.GetProductCategoryByName(name);
            if (productCategory == null) return NotFound();

            return Ok(productCategory);
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
    /// Add product category.
    /// </summary>
    /// <remarks>
    /// This endpoint will add product category.
    /// </remarks>
    /// <param name="productCategoryName"></param>
    /// <response code="201">Returns a newly added product category</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [Authorize]
    [Produces("application/json")]
    public async Task<ActionResult<ProductCategoryPayload>> AddProductCategory([FromBody] string productCategoryName)
    {
        try
        {
            var response = await _productCategoryService.AddProductCategory(productCategoryName);

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
    /// Update product category.
    /// </summary>
    /// <remarks>
    /// This endpoint returns will update product category.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="productCategoryName"></param>
    /// <response code="200">Returns a updated product category</response>
    /// <response code="401">If authentication is invalid</response>
    /// <response code="404">If no products are found</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPut]
    [Authorize]
    [Route("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateProductCategory([FromRoute, Required] int id, [FromBody] string productCategoryName)
    {
        try
        {
            await _productCategoryService.UpdateProductCategory(id, productCategoryName);
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
    /// Delete product category.
    /// </summary>
    /// <remarks>
    /// This endpoint returns will delete product category.
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
    public async Task<IActionResult> DeleteProduct([FromRoute, Required] int id)
    {
        try
        {
            await _productCategoryService.DeleteProductCategory(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
