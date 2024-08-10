using Domain.Entities;

namespace Domain.Repositories
{
    public interface IStoreRepository
    {
        void AddStore(Store store);
        void UpdateStore(Store store);
        void DeleteStore(Store store);
        Task<Store?> GetStoreByIdAsync(Guid id);
        Task<List<Store>> GetAllStoresAsync();
   }
}