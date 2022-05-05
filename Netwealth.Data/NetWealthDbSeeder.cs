using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Netwealth.Data;
using NetWealth.Data.Entities;
using NetWealth.Data.Models.Utility;


namespace NetWealth.Data
{
    public class NetWealthDbSeeder
    {
        private readonly NetwealthDbContext _ctx;
        private readonly CurrencyConverterSettings _currencyConverterSettings;

        public NetWealthDbSeeder(NetwealthDbContext ctx, IOptions<CurrencyConverterSettings> options)
        {
            _ctx = ctx;
            _currencyConverterSettings = options.Value;
        }

        public async Task Seed()
        {
            await _ctx.Database.EnsureCreatedAsync();


            if (!_ctx.ApiUsers.Any())
            {
                var user = new ApiUser()
                {
                    UserId = 12342,
                    Key = "19UbxmHgUZKryFLxQHV6VMzTP1bke7L0"
                };

                await _ctx.ApiUsers.AddAsync(user);

                await _ctx.SaveChangesAsync();
            }

            // Seed data from Api if Table is Empty
            if (!_ctx.CountryCurrencies.Any())
            {
                var uri = new Uri($"{_currencyConverterSettings.BaseUrl}symbols");
                var client = new RestClient(uri);

                var request = new RestRequest();
                request.AddHeader("apikey", _currencyConverterSettings.ApiKey);

                var response = await client.GetAsync(request);

                dynamic json = JsonConvert.DeserializeObject(response.Content);

                var sites = ((JObject)json["symbols"]).Properties();

                var seedCountries = new List<CountryCurrency>();

                foreach (var data in (JObject)json["symbols"])
                {
                    var key = data.Key;
                    var value = data.Value;

                    var country = new CountryCurrency()
                    {
                        Code = key,
                        Name = value.ToString(),
                    };

                    seedCountries.Add(country);

                }

                await _ctx.CountryCurrencies.AddRangeAsync(seedCountries);

                await _ctx.SaveChangesAsync();

            }
        }
    }
}
