using Domain.Repositories;
using Services.Contracts;
using AutoMapper;
using Shared.Dtos;
using Domain.Entities;
using Domain.Exceptions;

namespace Services
{
    public class StockItemService: IStockItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public StockItemService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StockItemDto> AddStockItem(StockItemForCreateDto stockItem)
        {
            StockItem entity = _mapper.Map<StockItem>(stockItem);

            if (await _repository.StoreRepository.GetStoreByIdAsync(stockItem.StoreId) is null)
                throw new BadRequestException("Store not exists.");
            else if (await _repository.ProductRepository.GetProductByIdAsync(stockItem.ProductId) is null)
                throw new BadRequestException("Product not exists.");
            else if (await _repository.StockItemRepository.GetStockItemFromStoreAsync(stockItem.StoreId, stockItem.ProductId) is not null)
                throw new BadRequestException("Stock item of product alredy exists in store.");

            _repository.StockItemRepository.AddStockItem(entity);
            await _repository.SaveAsync();

            return _mapper.Map<StockItemDto>(entity);
        }
        public async Task DeleteStockItem(Guid id)
        {
            StockItem? stockItem = await _repository.StockItemRepository.GetStockItemByIdAsync(id);

            if (stockItem is null)
                throw new StockItemNotFoundException(id);

            _repository.StockItemRepository.DeleteStockItem(stockItem);
            await _repository.SaveAsync();
        }
        public async Task<StockItemDto> AddItemsFromStockItem(Guid id, StockItemForUpdateDto stockItem)
        {
            StockItem? entity = await _repository.StockItemRepository.GetStockItemByIdAsync(id);

            if (entity is null)
                throw new StockItemNotFoundException(id);

            entity.Quantity += stockItem.Quantity;

            _repository.StockItemRepository.UpdateStockItem(entity);
            await _repository.SaveAsync();

            return _mapper.Map<StockItemDto>(entity);
        }

        public async Task<StockItemDto> RemoveItemsFromStockItem(Guid id, StockItemForUpdateDto stockItem)
        {
            StockItem? entity = await _repository.StockItemRepository.GetStockItemByIdAsync(id);

            if (entity is null)
                throw new StockItemNotFoundException(id);
            else if (stockItem.Quantity > entity.Quantity)
                throw new BadRequestException("Stack item quantity not enough.");

            entity.Quantity -= stockItem.Quantity;

            _repository.StockItemRepository.UpdateStockItem(entity);
            await _repository.SaveAsync();

            return _mapper.Map<StockItemDto>(entity);
        }

        public async Task<StockItemDto?> GetStockItem(Guid id)
        {
            StockItem? stockItem = await _repository.StockItemRepository.GetStockItemByIdAsync(id);

            if (stockItem is null)
                return null;

            return _mapper.Map<StockItemDto>(stockItem);
        }
    }
}
