using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Services.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/stockitem")]
    public class StockItemController : Controller
    {
        private readonly IServiceManager _service;

        public StockItemController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Register stock item of a product in store
        /// </summary>
        /// <param name="stockItem">Stock Item request body</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>The registered stock item</returns>

        [HttpPost(Name = "AddStockItem")]
        [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add([FromBody] StockItemForCreateDto stockItem)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(stockItem);

            StockItemDto created = await _service.StockItemService.AddStockItem(stockItem);
            return StatusCode(201, created);
        }

        /// <summary>
        /// Delete a stock item by id
        /// </summary>
        /// <param name="id">uuid id of stock item</param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <returns>Nothing</returns>

        [HttpDelete("{id:guid}", Name = "DeleteStockItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            await _service.StockItemService.DeleteStockItem(id);
            return NoContent();
        }

        /// <summary>
        /// Increase number of stock items
        /// </summary>
        /// <param name="id">uuid id of stock item</param>
        /// <param name="stockItem">Stock item request body</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>The updated stock item</returns>

        [HttpPut("additems/{id:guid}", Name = "AddItemsStockItem")]
        [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddItems([FromRoute] Guid id, [FromBody] StockItemForUpdateDto stockItem)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(stockItem);

            StockItemDto stockItemUpdated = await _service.StockItemService.AddItemsFromStockItem(id, stockItem);
            return Ok(stockItemUpdated);
        }

        /// <summary>
        /// Decrease number of stcok items
        /// </summary>
        /// <param name="id">uuid id of stock item</param>
        /// <param name="stockItem">Stock item request body</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>The updated stock item</returns>

        [HttpPut("removeitems/{id:guid}", Name = "RemoveItemsStockItem")]
        [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> RemoveItems([FromRoute] Guid id, [FromBody] StockItemForUpdateDto stockItem)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(stockItem);

            StockItemDto stockItemUpdated = await _service.StockItemService.RemoveItemsFromStockItem(id, stockItem);
            return Ok(stockItemUpdated);
        }

        /// <summary>
        /// Get a stock item by id
        /// </summary>
        /// <param name="id">uuid id of stock item</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <returns>Stock item with given id</returns>

        [HttpGet("{id:guid}", Name = "GetStockItemById")]
        [ProducesResponseType(typeof(StockItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            StockItemDto? stockItem = await _service.StockItemService.GetStockItem(id);
            return stockItem is null ? NotFound() : Ok(stockItem);
        }
    }
}
