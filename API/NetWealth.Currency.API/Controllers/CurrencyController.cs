using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetWealth.Data.Models.Query;
using NetWealth.Domain;

namespace NetWealth.Currency.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CurrencyController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private new readonly ILogger<CurrencyController> _logger;

        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) : base(mediator, logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [EnableCors("CurrencyPolicy")]
        [HttpGet("country")]
        public async Task<IActionResult> GetCountriesCurrencies()
        {
            var allCountryCurrencyDto = await _mediator.Send(new GetAllCountryCurrencyQuery());

            if (allCountryCurrencyDto == null)
            {
                var message = "There was an error retrieving Country Currency Codes";
                _logger.LogError($"Status Code {HttpStatusCode.InternalServerError.GetHashCode()} -{message}");
                return StatusCode(500, message);
            }

            return Ok(allCountryCurrencyDto);
        }

        [EnableCors("CurrencyPolicy")]
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertToCurrency([FromBody] CurrencyConverterCommand currencyConverterRequest)
        {
            List<string> errorMessages = new List<string>();
            if (currencyConverterRequest == null)
            {
                var message = "No data requested";
                errorMessages.Add(message); 
                _logger.LogError($"Status Code {HttpStatusCode.NoContent.GetHashCode()} - {message}");
                return BadRequest(message);
            }

            if (currencyConverterRequest.Amount > 0)
            {
                var message = "Amount must be set";
                _logger.LogError($"Status Code { HttpStatusCode.BadRequest.GetHashCode() } - {message}");
                errorMessages.Add(message);
            }
            if (String.IsNullOrEmpty(currencyConverterRequest.ToCurrencyReference))
            {
                var message = "Base Currency must be set";
                _logger.LogError($"Status Code { HttpStatusCode.BadRequest.GetHashCode() } - {message}");
                errorMessages.Add(message);
            }
            if (String.IsNullOrEmpty(currencyConverterRequest.FromCurrencyReference))
            {
                var message = " Base Currency must be set";
                _logger.LogError($"Status Code {HttpStatusCode.BadRequest.GetHashCode()} - {message}");
                errorMessages.Add(message);
            }

            if (errorMessages.Count > 0)
            {
                return BadRequest(String.Join('/', errorMessages.ToArray()));
            }

            CurrencyConverterResponse currencyConverterResponse;
            try
            {
                 currencyConverterResponse = await CommandAsync(currencyConverterRequest);
            }
            catch (Exception e)
            {
                var message = "There was an error while converting this currency";
                _logger.LogError($"{e.InnerException} - Stack Trace {e.StackTrace}");
                return StatusCode(500, message);
            }

            if (currencyConverterResponse.Success == false)
            {
                var message = "There was an error while converting this currency";
                return BadRequest(message);
            }

            return Ok(currencyConverterResponse);
        }
    }
}
