using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IStoreService StoreService { get; }
        IProductService ProductService { get; }
        IStockItemService StockItemService { get; }
    }
}
