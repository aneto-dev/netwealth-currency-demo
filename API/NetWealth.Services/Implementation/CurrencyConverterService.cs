using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetWealth.Data.Models.Query;
using NetWealth.Data.Models.Utility;
using Netwealth.Services;
using RestSharp;


namespace NetWealth.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly CurrencyConverterSettings _currencyConverterSettings;

        public CurrencyConverterService(IOptions<CurrencyConverterSettings> options)
        {
            _currencyConverterSettings = options.Value;
        }

        public async Task<CurrencyConverterResponse> GetCurrencyData(string toCurrencyConvertRequest, string baseCurrencyConvertRequest, decimal amount )
        {
            try
            {
                var uri = new Uri($"{_currencyConverterSettings.BaseUrl}convert?to={toCurrencyConvertRequest}&from={baseCurrencyConvertRequest}&amount={amount}");

                var clientTest = new RestClient(uri);

                var requestTest = new RestRequest();
                requestTest.AddHeader("apikey", _currencyConverterSettings.ApiKey);

                var responseTest = await clientTest.GetAsync<CurrencyConverterResponse>(requestTest);

                return responseTest;
            }
            catch (Exception e)
            {
                return new CurrencyConverterResponse() { Success = false, ResponseDescription = e.InnerException != null ? e.InnerException.ToString() : e.StackTrace };
            }
           
        }

    }
}
