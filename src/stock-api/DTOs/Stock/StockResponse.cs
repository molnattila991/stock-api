using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.DTOs.Stock
{
    public class StockResponse
    {
        public Dictionary<string, int> Value { get { return _response; } }

        private Dictionary<string, int> _response;

        public StockResponse(IEnumerable<StockItem> stockItem)
        {
            _response = new Dictionary<string, int>();
            foreach (var item in stockItem)
            {
                _response.Add(item.Type, item.Amount);
            }
        }
    }
}
