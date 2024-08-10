using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Services.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IServiceManager _service;

        public StoreController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id">uuid id of store</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <returns>The store with given id</returns>

        [HttpGet("{id:guid}", Name = "GetStoreById")]
        [ProducesResponseType(typeof(StoreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            StoreDto? store = await _service.StoreService.GetStore(id);
            return store is null ? NotFound() : Ok(store);
        }

        /// <summary>
        /// Register a store
        /// </summary>
        /// <param name="store">Store request body</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>The registered store</returns>

        [HttpPost(Name = "AddStore")]
        [ProducesResponseType(typeof(StoreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Add([FromBody] StoreForCreateDto store)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            StoreDto created = await _service.StoreService.AddStore(store);
            return StatusCode(201, created);
        }

        /// <summary>
        /// Update a store by id
        /// </summary>
        /// <param name="id">uiid id of store</param>
        /// <param name="store">store request body</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <returns>The updated store</returns>

        [HttpPut("{id:guid}",Name = "UpdateStoreById")]
        [ProducesResponseType(typeof(StoreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] StoreForUpdateDto store)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(store);

            StoreDto storeUpdated = await _service.StoreService.UpdateStore(id, store);
            return Ok(storeUpdated);
        }

        /// <summary>
        /// Returns a list of all stores
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">No Content</response>
        /// <returns>List of stores</returns>

        [HttpGet(Name = "GetAllStores")]
        [ProducesResponseType(typeof(List<StoreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IndexAll()
        {
            List<StoreDto> stores = await _service.StoreService.GetAllStores();
            return stores.Any() ? Ok(stores) : NoContent();
        }

        /// <summary>
        /// Delete a store by id
        /// </summary>
        /// <param name="id">uuid id of store</param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <returns>Nothing</returns>

        [HttpDelete("{id:guid}", Name = "DeleteStoreById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _service.StoreService.DeleteStore(id);
            return NoContent();
        }
    }
}
