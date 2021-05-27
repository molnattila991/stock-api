using Microsoft.AspNetCore.Mvc;
using stock_api.DTOs.Stock;
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
        [HttpPost]
        public async Task<IActionResult> Post(CreateStockRequest request)
        {
            var command = new StockCommand()
            {
                items = request.Inserted.Select(item => new StockItem()
                {
                    Amount = item.Value,
                    Type = item.Key,
                    ValueOfType = Convert.ToInt32(item.Key)
                })
            };

            await Mediator.Send(command);
            return Ok(await Mediator.Send(new StockGetAllQuery()));
        }
    }
}
