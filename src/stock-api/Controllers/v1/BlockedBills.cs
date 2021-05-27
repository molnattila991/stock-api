using Microsoft.AspNetCore.Mvc;
using stock_api.DTOs.Stock;
using stock_api.Validator;
using stock_api_application.Features.CheckOut.Commands;
using stock_api_application.Features.Stock.Queries;
using stock_api_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.Controllers.v1
{
    [ApiVersion("1.0")]

    public class BlockedBillsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new AcceptableItemsQuery()));
        }
    }
}
