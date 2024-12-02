using Microsoft.AspNetCore.Mvc;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    [SwaggerOperation(
            Summary = "List of Products",
            Description = "Get a list of product details.",
            OperationId = "getProducts"
        )]
    [ProducesResponseType(typeof(IEnumerable<ProductPayload>), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
            typeof(IEnumerable<ProductPayload>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<ProductPayload>>> GetProducts(
        string? search = null, string? sort = null, string? direction = "asc")
    {
        try
        {
            var products = await _productService.GetProducts();

            // Filter by search term
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // Sort by column
            if (!string.IsNullOrEmpty(sort))
            {
                products = sort.ToLower() switch
                {
                    "name" => direction == "asc"
                        ? products.OrderBy(p => p.Name)
                        : products.OrderByDescending(p => p.Name),
                    "description" => direction == "asc"
                        ? products.OrderBy(p => p.Description)
                        : products.OrderByDescending(_ => _.Description),
                    "category" => direction == "asc"
                        ? products.OrderBy(p => p.Category)
                        : products.OrderByDescending(p => p.Category),
                    "price" => direction == "asc"
                        ? products.OrderBy(p => p.Price)
                        : products.OrderByDescending(p => p.Price),
                    _ => products
                };
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/products/{id}
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(
            Summary = "Product Detail",
            Description = "Get a information of a product.",
            OperationId = "getProduct"
        )]
    [ProducesResponseType(typeof(ProductPayload), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
            typeof(ProductPayload))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<ProductPayload>> GetProduct([FromRoute] int id)
    {
        try
        {
            var product = await _productService.GetProduct(id);
            if (product == null) return NotFound();

            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/products
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Product",
        Description = "Create a Product",
        OperationId = "createProduct"
    )]
    [ProducesResponseType(typeof(ProductPayload), (int)HttpStatusCode.Created)]
    [SwaggerResponse((int)HttpStatusCode.Created, nameof(HttpStatusCode.Created),
        typeof(ProductPayload))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<ActionResult<ProductPayload>> PostProduct([FromBody] ProductPayload product)
    {
        try
        {
            var response = await _productService.PostProduct(product);

            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/products/{id}
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(
        Summary = "Update a Product",
        Description = "Update a Product",
        OperationId = "updateProduct"
    )]
    [ProducesResponseType(typeof(ProductPayload), (int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK),
        typeof(ProductPayload))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [Produces("application/json")]
    public async Task<IActionResult> PutProduct([FromRoute, Required] int id, [FromBody] ProductPayload product)
    {
        try
        {
            await _productService.PutProduct(id, product);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/products/{id}
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Product",
        Description = "Delete a Product",
        OperationId = "deleteProduct"
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
            await _productService.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
