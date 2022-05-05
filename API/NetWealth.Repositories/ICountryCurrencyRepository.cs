using System.Collections.Generic;
using System.Threading.Tasks;
using NetWealth.Data.Entities;

namespace NetWealth.Repositories
{
    public interface ICountryCurrencyRepository
    {
        Task<List<CountryCurrency>> GetCountryCurrencyData();
    }
}