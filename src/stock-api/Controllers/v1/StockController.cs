using Microsoft.AspNetCore.Mvc;
using stock_api.DTOs.Stock;
using stock_api.Validator;
using stock_api_application.Features.Stock.Commands;
using stock_api_application.Features.Stock.Queries;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StockController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new StockGetAllQuery());
            return Ok(new StockResponse(result).Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateStockRequest request)
        {
            StockDictionaryValidator.Validate(request.Inserted);

            var command = new StockCommand()
            {
                Items = request.Inserted.Select(item => new StockItem()
                {
                    Amount = item.Value,
                    Type = item.Key,
                    ValueOfType = Convert.ToInt32(item.Key)
                })
            };

            await Mediator.Send(command);

            var result = await Mediator.Send(new StockGetAllQuery());
            return Ok(new StockResponse(result).Value);
        }
    }
}
