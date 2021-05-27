using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.DTOs.Stock
{
    public class CheckOutStockRequest
    {
        public Dictionary<string, int> Inserted { get; set; }
        public int Price { get; set; }

    }
}
