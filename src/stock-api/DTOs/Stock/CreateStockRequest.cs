using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.DTOs.Stock
{
    public class CreateStockRequest
    {
        public Dictionary<string, int> Inserted { get; set; }
    }
}
