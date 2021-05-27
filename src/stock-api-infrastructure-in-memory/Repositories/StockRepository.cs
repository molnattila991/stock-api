using stock_api_application.Exceptions;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_infrastructure_in_memory.Repositories
{
    public class StockRepository : IStockRepository
    {
        private  List<StockItem> _stock;
        private object _lockObject;

        public StockRepository()
        {
            _lockObject = new object();
            _stock = new List<StockItem>();
        }

        public Task AddItem(StockItem item)
        {
            lock (_lockObject)
            {
                var result = GetByType(item.Type);
                if(result == null)
                {
                    _stock.Add(item);
                }
                else
                {
                    result.Amount += item.Amount;
                }
            }
            return Task.CompletedTask;
        }

        public async Task AddItems(IEnumerable<StockItem> items)
        {
            foreach (var item in items)
            {
                await this.AddItem(item);
            }
        }

        public Task<IEnumerable<StockItem>> GetItems()
        {
            return Task.FromResult<IEnumerable<StockItem>>(_stock.ToList());
        }

        public Task RemoveItem(StockItem item)
        {
            lock (_lockObject)
            {
                var result = GetByType(item.Type);
                if (result == null)
                {
                    throw new MoneyWithTypeIsNotInStockException($"Money with type {item.Type} is not in stock.");
                }
                else
                {
                    if (result.Amount >= item.Amount)
                    {
                        result.Amount -= item.Amount;
                    }
                    else
                    {
                        throw new NotEnoughTypeOfMoneyInStock();
                    }
                    result.Amount += item.Amount;
                }
            }

            return Task.CompletedTask;
        }

        public async Task RemoveItems(IEnumerable<StockItem> items)
        {
            foreach (var item in items)
            {
                await this.RemoveItem(item);
            }
        }

        private StockItem GetByType(string type)
        {
            return _stock.FirstOrDefault(s => s.Type == type);
        }
    }
}
