using MediatR;
using stock_api_domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using stock_api_application.Exceptions;
using stock_api_application.Interfaces;

namespace stock_api_application.Features.Stock.Commands
{
    public class StockCommand : IRequest<bool>
    {
        public IEnumerable<StockItem> Items { get; set; }
    }

    public class StockCommandHandler : IRequestHandler<StockCommand, bool>
    {
        private readonly IStockAddRepository _stockRepository;
        private readonly IValidateIncomingItems _validateIncomingItems;

        public StockCommandHandler(IStockRepository stockRepository, IValidateIncomingItems validateIncomingItems)
        {
            _stockRepository = stockRepository;
            _validateIncomingItems = validateIncomingItems;
        }

        public async Task<bool> Handle(StockCommand request, CancellationToken cancellationToken)
        {
            if (_validateIncomingItems.ValidatedItems(request.Items) == false)
            {
                throw new NotValidCurrencyException();
            }

            await _stockRepository.AddItems(request.Items);
            return true;
        }
    }
}
