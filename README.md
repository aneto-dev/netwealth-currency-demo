# netwealth-currency-demo

# Angular, ASP.NET Core Currency Converter Application

This project demonstrates how Angular and ASP.NET Core can be used together.

## Run Locally

* The project will run and can be debuged on the angular (netwealth-currency)
* The angular project will make calls to a second main project (NetWealth.Currency.API) which is an API project using .Net Core 5.0
* The API was deplyed in Azure cloud and it has public access ("https://netwealthcurrencyapi.azurewebsites.net") ... for porpose of this demo only

### Running the API

1. The API has 2 endpoints

   * GetCountriesCurrencies - gets all the current symbol currency codes of a given country. This data is stored and is fetched in an Azure Database that was also created for purposes of this demo.
   * ConvertToCurrency  -  claculates/conveters any given available currency using the https://fixer.io/ to fectch live data 

2. API Key

   * A unique key was added in order to access the endpoints ... this is to ensure that if this endpoint was to be given to anyone they would need a key to access data

   * API KEY  - 19UbxmHgUZKryFLxQHV6VMzTP1bke7L0

   * This key is also stored in the Azure DB so it can matched against (if needed)

### More Improvments (If given more time)

* A second layer of authentication/security could be added using a token that could be refeshed in a given span time
* Roles could also be added to the api to restrict specific users to accces certain endpoints  (assuming that potentially more could be added) 
* More frontend styling
* More frontend testing

