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

            _stock.AddRange(new List<StockItem>() {
                new StockItem() { ValueOfType = 5, Type = "5", Amount = 10 },
                new StockItem() { ValueOfType = 10, Type = "10", Amount = 10 },
                new StockItem() { ValueOfType = 20, Type = "20", Amount = 10 },
                new StockItem() { ValueOfType = 50, Type = "50", Amount = 10 },
                //new StockItem() { ValueOfType = 100, Type = "100", Amount = 10 },
                //new StockItem() { ValueOfType = 200, Type = "200", Amount = 10 },
                //new StockItem() { ValueOfType = 500, Type = "500", Amount = 10 },
                //new StockItem() { ValueOfType = 1000, Type = "1000", Amount = 10 },
                //new StockItem() { ValueOfType = 2000, Type = "2000", Amount = 10 },
                //new StockItem() { ValueOfType = 5000, Type = "5000", Amount = 10 },
                //new StockItem() { ValueOfType = 10000, Type = "10000", Amount = 10 },
                //new StockItem() { ValueOfType = 20000, Type = "20000", Amount = 10 },
            });
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
                await AddItem(item);
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
                        if (result.Amount == 0)
                        {
                            _stock.Remove(result);
                        }
                    }
                    else
                    {
                        throw new NotEnoughTypeOfMoneyInStock();
                    }
                }
            }

            return Task.CompletedTask;
        }

        public async Task RemoveItems(IEnumerable<StockItem> items)
        {
            foreach (var item in items)
            {
                await RemoveItem(item);
            }
        }

        private StockItem GetByType(string type)
        {
            return _stock.FirstOrDefault(s => s.Type == type);
        }
    }
}
