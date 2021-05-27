using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace stock_api_application.Services
{
    public class CalculatedValue
    {
        public int Remaining { get; set; }
        public List<StockItem> Items { get; set; }
    }

    public class ChangeCalculator
    {
        private readonly IStockQueryRepository _stockRepository;

        public ChangeCalculator(IStockQueryRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public bool ValueIsChangeble(int value) => CalculateChanges(value).Remaining == 0;

        public CalculatedValue CalculateChanges(int value)
        {
            CalculatedValue result = new CalculatedValue()
            {
                Items = new List<StockItem>(),
                Remaining = value
            };

            var itemsInStock = _stockRepository.GetItems().Result;
            var orderedList = itemsInStock.Where(item => item.ValueOfType <= value).OrderByDescending(item => item.ValueOfType);

            int index = 0;
            while (index < orderedList.Count() && result.Remaining > 0)
            {
                var item = orderedList.ElementAt(index);
                result.Remaining = Calculate(result.Items, result.Remaining, item);
                index++;
            }

            return result;
        }

        private int Calculate(List<StockItem> changeList, int value, StockItem item)
        {
            int requiredAmount = value / item.ValueOfType;
            int usableAmount = Clamp(requiredAmount, item.Amount);

            if (requiredAmount > 0 && usableAmount > 0)
            {
                value -= usableAmount * item.ValueOfType;

                changeList.Add(new StockItem()
                {
                    Amount = usableAmount,
                    Type = item.Type,
                    ValueOfType = item.ValueOfType
                });
            }

            return value;
        }

        private int Clamp(int value, int max) => value <= max ? value : max;
    }
}
