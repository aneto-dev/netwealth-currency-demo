
# Angular/ASP.NET Core Currency Converter Demo Application 

This project demonstrates how Angular and ASP.NET Core can be used together.

## Run Locally

* The project will run and can be debuged client-side (netwealth-currency-app)
* The angular project will make calls to a second main project (API/NetWealth.Currency.API) which is an API project using .Net Core 5.0
* The API was deployed in Azure and it has public access ("https://netwealthcurrencyapi.azurewebsites.net") - for porpose of this demo only
* The Web Application is also deployed in Azure - https://currencyconverterapp.azurewebsites.net/

### Running the API

1. The API has 2 endpoints

   ## HTTP request GET /currency/country
   * GetCountriesCurrencies 
        - gets all the current symbol currency codes of a given country. This data is stored and is fetched from an Azure Database that was also created for purposes of this demo.

   ## HTTP request POST /currency/convert
   * ConvertToCurrency  
        - claculates/conveters any given available currency using the https://fixer.io/ to fectch live data 

2. API Key

   * A unique key was added in order to access the endpoints. This is to ensure that if this endpoint was to be given to anyone they would need a key to access data

   * API KEY  - 19UbxmHgUZKryFLxQHV6VMzTP1bke7L0

   * This key is also stored in the Azure DB so it can matched against (if needed)

### Improvments (If given more time)

* A second layer of authentication/security could be added using a token that could be refeshed in a given span time
* Roles could also be added to the api to restrict specific users to accces certain endpoints  (assuming that potentially more could be added) 
* More frontend styling
* More testing scenarios (backend/frontend)

