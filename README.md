# netwealth-currency-demo

# Angular, ASP.NET Core Currency Converter Application

This project demonstrates how Angular and ASP.NET Core can be used together.

## Run Locally

* The project will run and can be debuged on the agular (netwealth-currency)
* The angular project will make calls to the second main project (NetWealth.Currency.API) which is a API project .Net Core 5.0
* The API is deplyed in Azure cloud and it has public access ("https://netwealthcurrencyapi.azurewebsites.net") ... for porpose of this demo only

### Running the API

1. The API has 2 endpoints

   * GetCountriesCurrencies - gets all the current symbol currency codes of a given country. This data is stored and is fetched in an Azure Database that was also created for purposes of this demo.
   * ConvertToCurrency  -  claculates/conveters any given available currency using the https://fixer.io/ to fectch live data 

