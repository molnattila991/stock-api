using Microsoft.AspNetCore.Mvc;
using stock_api.DTOs.Stock;
using stock_api.Validator;
using stock_api_application.Features.CheckOut.Commands;
using stock_api_domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.Controllers.v1
{
    [ApiVersion("1.0")]

    public class CheckOutController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CheckOutStockRequest request)
        {
            StockDictionaryValidator.Validate(request.Inserted);

            var command = new CheckOutStockCommand()
            {
                Items = request.Inserted.Select(item => new StockItem()
                {
                    Amount = item.Value,
                    Type = item.Key,
                    ValueOfType = Convert.ToInt32(item.Key)
                }),
                Price = request.Price
            };

            var result = await Mediator.Send(command);
            return Ok(new StockResponse(result).Value);
        }
    }
}
