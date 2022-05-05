using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [HttpGet("country")]
        public async Task<IActionResult> GetCountriesCurrencies()
        {
            var allCountryCurrencyDto = await _mediator.Send(new GetAllCountryCurrencyQuery());

            if (allCountryCurrencyDto == null)
                return StatusCode(500, "There was an error retrieving Country Currency Codes");

            return Ok(allCountryCurrencyDto);
        }

        [EnableCors("CurrencyPolicy")]
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertToCurrency([FromBody] CurrencyConverterCommand currencyConverterRequest)
        {
            if (currencyConverterRequest == null)
                return BadRequest("No data requested");
            if (currencyConverterRequest.ToCurrencyReference == null)
                return BadRequest("Currency to be converted should be set");
            if (currencyConverterRequest.FromCurrencyReference == null)
                return BadRequest("Base Currency should be set");


            var currencyConverterResponse = await CommandAsync(currencyConverterRequest);
            return Ok(currencyConverterResponse);
        }
    }
}
