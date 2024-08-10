using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        IStoreRepository StoreRepository { get; }
        IStockItemRepository StockItemRepository { get; }
        Task SaveAsync();
    }
}
