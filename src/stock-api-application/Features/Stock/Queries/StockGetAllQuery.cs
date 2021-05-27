using MediatR;
using stock_api_application.Interfaces;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stock_api_application.Features.Stock.Queries
{
    public class StockGetAllQuery : IRequest<IEnumerable<StockItem>>
    {

    }

    public class StockGetAllQueryHandler : IRequestHandler<StockGetAllQuery, IEnumerable<StockItem>>
    {
        private readonly IStockQueryRepository _stockRepository;

        public StockGetAllQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<StockItem>> Handle(StockGetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _stockRepository.GetItems();

            return result;
        }
    }
}
