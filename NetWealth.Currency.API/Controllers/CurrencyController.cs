using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetWealth.Data.Entities;
using NetWealth.Data.Models.Command;
//using NetWealth.Data.Models.Command;
using NetWealth.Domain;

namespace NetWealth.Currency.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CurrencyController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) : base(mediator, logger)
        {
            _mediator = mediator;
        }

        [EnableCors("CurrencyPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allCountryCurrencyDto = await _mediator.Send(new GetAllCountryCurrencyQuery());
            return Ok(allCountryCurrencyDto);
        }

        [EnableCors("CurrencyPolicy")]
        [HttpPost]
        public async Task<IActionResult> GetCurrency([FromBody] CurrencyConverterCommand currencyConverterRequest)
        {
            var currencyConverterResponse = await CommandAsync(currencyConverterRequest);
            return Ok(currencyConverterResponse);
        }
    }
}
