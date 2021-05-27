using MediatR;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stock_api_application.Features.CheckOut.Commands
{
    public class CheckOutStockCommand : IRequest<IEnumerable<StockItem>>
    {
        public IEnumerable<StockItem> items { get; set; }
        public int Price { get; set; }
    }

    public class CheckOutStockCommandHandler : IRequestHandler<CheckOutStockCommand, IEnumerable<StockItem>>
    {
        public Task<IEnumerable<StockItem>> Handle(CheckOutStockCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
