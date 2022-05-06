using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWealth.Data.Models.Dto
{
    public class CurrencyConverterResponseDto
    {
        public DateTime Date { get; set; }
        public decimal Result { get; set; }
        public QueryDto Query { get; set; }
        public InfoDto Info { get; set; }

        public bool Success { get; set; }

        public string ResponseDescription { get; set; }

        public string CurrencyDescription { get; set; }
    }

    public class QueryDto
    {
        public decimal Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class InfoDto
    {
        public decimal BaseRate { get; set; }
        public decimal Rate { get; set; }
        public int TimeStamp { get; set; }
    }
}
