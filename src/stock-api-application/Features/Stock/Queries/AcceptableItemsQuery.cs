using MediatR;
using stock_api_application.Interfaces;
using stock_api_application.Services;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stock_api_application.Features.Stock.Queries
{
    public class AcceptableItemsQuery : IRequest<IEnumerable<ValidItem>>
    {
    }

    public class AcceptableItemsQueryHandler : IRequestHandler<AcceptableItemsQuery, IEnumerable<ValidItem>>
    {
        private readonly IStockQueryRepository _stockRepository;
        private readonly IValidItemRepository _validItemRepository;


        public AcceptableItemsQueryHandler(IStockRepository stockRepository, IValidItemRepository validItemRepository)
        {
            _stockRepository = stockRepository;
            _validItemRepository = validItemRepository;
        }

        public async Task<IEnumerable<ValidItem>> Handle(AcceptableItemsQuery request, CancellationToken cancellationToken)
        {
            var validItems = await _validItemRepository.GetItems();
            var calculator = new ChangeCalculator(_stockRepository);
            var resultList = new List<ValidItem>();

            foreach (var item in validItems)
            {
                if(calculator.ValueIsChangeble(item.ValueOfType))
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }
    }
}
