using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netwealth.Data;
using NetWealth.Data.Entities;

namespace NetWealth.Repositories.Implementation
{
    public class CountryCurrencyRepository : ICountryCurrencyRepository
    {
        private readonly NetwealthDbContext _netWealthDbContext;

        public CountryCurrencyRepository(NetwealthDbContext netWealthDbContext)
        {
            _netWealthDbContext = netWealthDbContext;
        }
         
        public async Task<List<CountryCurrency>> GetCountryCurrencyData( )
        {
            return await _netWealthDbContext.CountryCurrencies.ToListAsync();
        }
    }
}
