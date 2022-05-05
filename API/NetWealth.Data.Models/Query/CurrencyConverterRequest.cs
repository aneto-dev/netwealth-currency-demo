using MediatR;

namespace NetWealth.Data.Models.Query
{
    public class CurrencyConverterRequest : IRequest<CurrencyConverterResponse>, INotification
    {
        public decimal Amount { get; set; }
        public string FromCurrencyReference { get; set; }
        public string ToCurrencyReference { get; set; }

    }
}
