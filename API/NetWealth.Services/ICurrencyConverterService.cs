using System.Threading.Tasks;
using NetWealth.Data.Models.Query;

namespace Netwealth.Services
{
    public interface ICurrencyConverterService
    {
        Task<CurrencyConverterResponse> GetCurrencyData(string toCurrencyConvertRequest,
            string baseCurrencyConvertRequest, decimal amount);

    }
}
