using MediatR;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stock_api_application.Features.CheckOut.Commands
{
    public class CheckOutStockCommand : IRequest<IEnumerable<StockItem>>
    {
        public IEnumerable<StockItem> Items { get; set; }
        public int Price { get; set; }
    }

    public class CheckOutStockCommandHandler : IRequestHandler<CheckOutStockCommand, IEnumerable<StockItem>>
    {
        private readonly IStockRepository _stockRepository;

        public CheckOutStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<StockItem>> Handle(CheckOutStockCommand request, CancellationToken cancellationToken)
        {
            int sumOfItems = request.Items.Sum(item => item.ValueOfType * item.Amount);
            int newPrice = RoundPrice(request.Price);

            int diff = sumOfItems - newPrice;

            if (diff < 0)
            {
                throw new Exception("Have not given enough money.");
            }
            else
            {
                await _stockRepository.AddItems(request.Items);
                if (diff == 0)
                {
                    return new List<StockItem>();
                }
                else
                {
                    List<StockItem> changeList = await HandleChanges(diff);
                    return changeList;
                }
            }
        }

        private async Task<List<StockItem>> HandleChanges(int diff)
        {
            var itemsInStock = await _stockRepository.GetItems();
            var orderedList = itemsInStock.Where(item => item.ValueOfType <= diff).OrderByDescending(item => item.ValueOfType);
            var changeList = new List<StockItem>();

            int copyOfDiff = CalculateChanges(orderedList, changeList, diff);

            if (copyOfDiff > 0)
            {
                throw new Exception("Not enought change in stock.");
            }
            else
            {
                await _stockRepository.RemoveItems(changeList);
            }

            return changeList;
        }

        private int CalculateChanges(IOrderedEnumerable<StockItem> orderedList, List<StockItem> changeList, int diff)
        {
            int copyOfDiff = diff;
            int index = 0;
            while (index < orderedList.Count() && copyOfDiff > 0)
            {
                var item = orderedList.ElementAt(index);
                int amount = copyOfDiff / item.ValueOfType;
                int usableAmount = Clamp(amount, item.Amount);

                if (amount > 0 && usableAmount > 0)
                {
                    copyOfDiff -= usableAmount * item.ValueOfType;

                    changeList.Add(new StockItem()
                    {
                        Amount = usableAmount,
                        Type = item.Type,
                        ValueOfType = item.ValueOfType
                    });
                }

                index++;
            }

            return copyOfDiff;
        }

        /// <summary>
        /// Round the given price in case the end is 1,2,3,4,6,7,8,9
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private int RoundPrice(int price)
        {
            var round = price % 5;
            if (round >= 3)
            {
                return price + (5 - round);
            }
            else
            {
                return price - round;
            }
        }

        public int Clamp(int value, int max)
        {
            if(value <= max)
            {
                return value;
            } 
            else
            {
                return max;
            }
        }
    }
}
