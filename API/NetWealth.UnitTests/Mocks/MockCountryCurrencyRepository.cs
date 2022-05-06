using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NetWealth.Data.Entities;
using NetWealth.Repositories;

namespace NetWealth.UnitTests.Mocks
{
    public static class MockCountryCurrencyRepository 
    {
        public static Mock<ICountryCurrencyRepository> GetCountryCurrencyData()
        {
            var list = new List<CountryCurrency>()
            {

                 new CountryCurrency() { Code = "EUR", Name = "Euros_TEST", Reference = 1}, 
                 new CountryCurrency() { Code = "USD", Name = "US Dollars_TEST", Reference = 2},
                 new CountryCurrency() { Code = "JPY", Name = "Japanese Yen_TEST", Reference = 3},
                 new CountryCurrency() { Code = "GBP", Name = "British Pounds_TEST", Reference = 4},

            };

            var mockRepo = new Mock<ICountryCurrencyRepository>();
            mockRepo.Setup(r => r.GetCountryCurrencyData()).ReturnsAsync(list);

            return mockRepo;
        }
    }
}
