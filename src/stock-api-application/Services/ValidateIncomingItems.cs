using stock_api_application.Exceptions;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_api_application.Services
{
    public class ValidateIncomingItems : IValidateIncomingItems
    {
        private readonly IValidItemRepository _validItemRepository;

        public ValidateIncomingItems(IValidItemRepository validItemRepository)
        {
            _validItemRepository = validItemRepository;
        }

        public bool ValidatedItems(IEnumerable<StockItem> items)
        {
            var validItems = _validItemRepository.GetItems().Result;
            if (items.Where(item => validItems.Any(validItem => validItem.Type == item.Type) == false).Count() > 0)
            {
                return false;
            }

            return true;
        }
    }
}
