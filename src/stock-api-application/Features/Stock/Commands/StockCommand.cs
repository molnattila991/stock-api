using MediatR;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stock_api_application.Features.Stock.Commands
{
    public class StockCommand : IRequest<bool>
    {
        public IEnumerable<StockItem> items { get; set; }
    }

    public class StockCommandHandler : IRequestHandler<StockCommand, bool>
    {
        private readonly IStockAddRepository _stockRepository;

        public StockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<bool> Handle(StockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _stockRepository.AddItems(request.items);
            }
            catch (Exception e)
            {
                //TODO log
                return false;
            }
            return true;
        }
    }
}
