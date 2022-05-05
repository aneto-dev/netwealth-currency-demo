using System;

namespace NetWealth.Data.Models.Query
{
    public class CurrencyConverterResponse
    {
        public DateTime Date { get; set; }
        public decimal Result { get; set; }
        public Query Query { get; set; }
        public Info Info { get; set; }

        public bool Success { get; set; }

        public string CurrencyDescription { get; set; }

        public decimal ToCurrencyCurrentValue { get; set; }

        public decimal FromCurrencyCurrentValue { get; set; }
    }

    public class Query
    {
        public decimal Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Info
    {
        public decimal Rate { get; set; }
        public int TimeStamp { get; set; }
    }
}
