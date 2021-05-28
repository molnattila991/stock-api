using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using stock_api_application.Exceptions;
using stock_api_application.Interfaces;
using stock_api_application.Services;
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
        private readonly IValidateIncomingItems _validateIncomingItems;
        private readonly ILogger<CheckOutStockCommandHandler> _logger;

        public CheckOutStockCommandHandler(
            IStockRepository stockRepository, 
            IValidateIncomingItems validateIncomingItems,
            ILogger<CheckOutStockCommandHandler> logger
            )
        {
            _stockRepository = stockRepository;
            _validateIncomingItems = validateIncomingItems;
            _logger = logger;
        }

        public async Task<IEnumerable<StockItem>> Handle(CheckOutStockCommand request, CancellationToken cancellationToken)
        {
            if(_validateIncomingItems.ValidatedItems(request.Items)  == false)
            {
                _logger.LogError(typeof(NotValidCurrencyException).ToString());
                throw new NotValidCurrencyException();
            }

            int sumOfItems = request.Items.Sum(item => item.ValueOfType * item.Amount);
            int newPrice = HUFHelper.RoundPrice(request.Price);

            int difference = sumOfItems - newPrice;

            if (difference < 0)
            {
                _logger.LogError("Have not given enough money.");
                throw new ChangeException("Have not given enough money.");
            }
            else
            {
                await _stockRepository.AddItems(request.Items);
                if (difference == 0)
                {
                    return new List<StockItem>();
                }
                else
                {
                    List<StockItem> changeList = await HandleChanges(difference);
                    return changeList;
                }
            }
        }

        private async Task<List<StockItem>> HandleChanges(int value)
        {
            var calculator = new ChangeCalculator(_stockRepository);
            var result = calculator.CalculateChanges(value);

            if (result.Remaining > 0)
            {
                _logger.LogError("Not enought change in stock.");
                throw new ChangeException("Not enought change in stock.");
            }
            else
            {
                await _stockRepository.RemoveItems(result.Items);
            }

            return result.Items;
        }
    }
}
