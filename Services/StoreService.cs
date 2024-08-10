using Domain.Repositories;
using Services.Contracts;
using AutoMapper;
using Shared.Dtos;
using Domain.Entities;
using Domain.Exceptions;

namespace Services
{
    public class StoreService : IStoreService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public StoreService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StoreDto> AddStore(StoreForCreateDto store)
        {
            Store entity = _mapper.Map<Store>(store);

            _repository.StoreRepository.AddStore(entity);
            await _repository.SaveAsync();

            return _mapper.Map<StoreDto>(entity);
        }

        public async Task<StoreDto> UpdateStore(Guid id, StoreForUpdateDto store)
        {
            Store? entity = await _repository.StoreRepository.GetStoreByIdAsync(id);

            if (entity is null)
                throw new StoreNotFoundException(id);

            _mapper.Map(store, entity, typeof(StoreForUpdateDto), typeof(Store));

            _repository.StoreRepository.UpdateStore(entity);
            await _repository.SaveAsync();

            return _mapper.Map<StoreDto>(entity);
        }

        public async Task DeleteStore(Guid id)
        {
            Store? store = await _repository.StoreRepository.GetStoreByIdAsync(id);

            if (store is null)
                throw new StoreNotFoundException(id);

            _repository.StoreRepository.DeleteStore(store);
            await _repository.SaveAsync();
        }

        public async Task<StoreDto?> GetStore(Guid id)
        {
            Store? store = await _repository.StoreRepository.GetStoreByIdAsync(id);

            if (store is null)
                return null;

            return _mapper.Map<StoreDto>(store);
        }

        public async Task<List<StoreDto>> GetAllStores()
        {
            List<Store> stores = await _repository.StoreRepository.GetAllStoresAsync();

            return _mapper.Map<List<StoreDto>>(stores);
        }
    }
}
