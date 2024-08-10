using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Services.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the list of all products
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">No Content</response>
        /// <returns>products list</returns>

        [HttpGet(Name ="GetAllProducts")]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IndexAll()
        {
            List<ProductDto> products = await _service.ProductService.GetAllProducts();
            return products.Any() ? Ok(products) : NoContent();
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">uuid id of product</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <returns>Return a product</returns>

        [HttpGet("{id:guid}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            ProductDto? product = await _service.ProductService.GetProduct(id);
            return product is null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Register product in database
        /// </summary>
        /// <param name="product">Product request body</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>Return the registered product</returns>

        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add([FromBody] ProductForCreateDto product)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(product);

            ProductDto created = await _service.ProductService.AddProduct(product);
            return StatusCode(201, created);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">uuid id of product</param>
        /// <param name="product">Product request body</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>Return the updated product</returns>

        [HttpPut("{id:guid}", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductForUpdateDto product)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(product);

            ProductDto productUpdated = await _service.ProductService.UpdateProduct(id, product);
            return Ok(productUpdated);
        }

        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="id">uuid id of product</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns>None</returns>

        [HttpDelete("{id:guid}", Name = "DeleteProductById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            await _service.ProductService.DeleteProduct(id);
            return NoContent();
        }
    }
}
