using Domain.Repositories;
using Shared.Dtos;

namespace Services.Contracts
{
    public interface IStoreService
    {
        Task<StoreDto> AddStore(StoreForCreateDto store);
        Task<StoreDto> UpdateStore(Guid id, StoreForUpdateDto store);
        Task DeleteStore(Guid id);
        Task<StoreDto?> GetStore(Guid id);
        Task<List<StoreDto>> GetAllStores();
    }
}
