using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_application.Interfaces
{
    public interface IValidateIncomingItems
    {
        bool ValidatedItems(IEnumerable<StockItem> items);
    }
}
