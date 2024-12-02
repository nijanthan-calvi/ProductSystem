using Microsoft.AspNetCore.Mvc;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoryController : Controller
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    // GET: api/productCategories
    [HttpGet]
    [SwaggerOperation(
            Summary = "List of Product Categories",
            Description = "Get a list of product category details.",
            OperationId = "getProductCategories"
        )]
    [ProducesResponseType(typeof(IEnumerable<ProductCategoryPayload>), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
            typeof(IEnumerable<ProductCategoryPayload>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductPayload>>> GetProducts()
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

    // GET: api/productcategory/{name}
    [HttpGet]
    [Route("{name}")]
    [SwaggerOperation(
            Summary = "Product Category Detail",
            Description = "Get a information of a product category.",
            OperationId = "getProductCategory"
        )]
    [ProducesResponseType(typeof(ProductCategoryPayload), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
            typeof(ProductCategoryPayload))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<ProductCategoryPayload>> GetProductCategory([FromRoute] string name)
    {
        try
        {
            var productCategory = await _productCategoryService.GetProductCategoryByName(name);
            if (productCategory == null) return NotFound();

            return Ok(productCategory);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/productcategory
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Product Category",
        Description = "Create a Product Category",
        OperationId = "createProductCategory"
    )]
    [ProducesResponseType(typeof(ProductCategoryPayload), (int)HttpStatusCode.Created)]
    [SwaggerResponse((int)HttpStatusCode.Created, nameof(HttpStatusCode.Created),
        typeof(ProductCategoryPayload))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<ProductCategoryPayload>> PostProduct([FromBody] string productCategoryName)
    {
        try
        {
            var response = await _productCategoryService.AddProductCategory(productCategoryName);

            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/productcategory/{id}
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(
        Summary = "Update a Product Category",
        Description = "Update a Product Category",
        OperationId = "updateProductCategory"
    )]
    [ProducesResponseType(typeof(ProductCategoryPayload), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
        typeof(ProductCategoryPayload))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<IActionResult> PutProduct([FromRoute, Required] int id, [FromBody] string productCategoryName)
    {
        try
        {
            await _productCategoryService.UpdateProductCategory(id, productCategoryName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/productcategory/{id}
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Product Category",
        Description = "Delete a Product Category",
        OperationId = "deleteProductCategory"
    )]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [SwaggerResponse((int)HttpStatusCode.NoContent, nameof(HttpStatusCode.NoContent))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
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
