using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_application.Interfaces
{
    public interface IStockAddRepository
    {
        Task AddItem(StockItem item);
        Task AddItems(IEnumerable<StockItem> items);
    }

    public interface IStockRemoveRepository
    {
        Task RemoveItem(StockItem item);
        Task RemoveItems(IEnumerable<StockItem> items);
    }

    public interface IStockQueryRepository
    {
        Task<IEnumerable<StockItem>> GetItems();
    }

    public interface IStockRepository : IStockAddRepository, IStockRemoveRepository, IStockQueryRepository
    {
    }
}
