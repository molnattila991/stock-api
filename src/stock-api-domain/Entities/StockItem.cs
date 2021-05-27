using System;
using System.Collections.Generic;
using System.Text;

namespace stock_api_domain.Entities
{
    public class StockItem
    {
        public string Type { get; set; }
        public int ValueOfType { get; set; }
        public int Amount { get; set; }
    }
}
