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

            // Seed data from Api if Table is Empty
            if (!_ctx.Countries.Any())
            {
                var uri = new Uri($"{_currencyConverterSettings.BaseUrl}symbols");
                var client = new RestClient(uri);

                var request = new RestRequest();
                request.AddHeader("apikey", _currencyConverterSettings.ApiKey);

                var response = await client.GetAsync(request);

                dynamic json = JsonConvert.DeserializeObject(response.Content);

                var sites = ((JObject)json["symbols"]).Properties();

                var seedCountries = new List<Country>();    

                foreach (var data in (JObject)json["symbols"])
                {
                    var key = data.Key;
                    var value = data.Value;

                    var country = new Country()
                    {
                        Code = key, 
                        Name = value.ToString(Formatting.None),
                    };

                    seedCountries.Add(country);

                }

                await _ctx.Countries.AddRangeAsync(seedCountries);

                await _ctx.SaveChangesAsync();   

            }
        }
    }
}
