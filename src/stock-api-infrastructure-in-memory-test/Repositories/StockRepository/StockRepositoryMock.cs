using NUnit.Framework;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_infrastructure_in_memory_test.Repositories
{
    static class StockRepositoryMock
    {
        public static IEnumerable<StockItem> GetDefaultStockItemList() => new List<StockItem>() {
                new StockItem() { ValueOfType = 5, Type = "5", Amount = 10 },
                new StockItem() { ValueOfType = 10, Type = "10", Amount = 10 },
                new StockItem() { ValueOfType = 20, Type = "20", Amount = 10 },
                new StockItem() { ValueOfType = 50, Type = "50", Amount = 10 }
            };

        public static void CheckList(IEnumerable<StockItem> a, IEnumerable<StockItem> b)
        {
            for (int i = 0; i < a.Count(); i++)
            {
                Assert.AreEqual(a.ElementAt(i), b.ElementAt(i));
            }
        }
    }
}
