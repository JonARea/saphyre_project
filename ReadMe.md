# SignalR Stockticker

This is an mvp for a Stockticker service that sends users price updates on stocks they choose.
It uses the Yahoo Finance API to get realtime stock information.
It is built with ASP.NET Core, SignalR, and React

## Running the app

This app requires credentials for the YahooFinance Api, available through RapidApi https://rapidapi.com/apidojo/api/yahoo-finance1

1. Set the environment variables API_KEY, API_HOST, and API_BASE_ADDRESS to the api credentials and url.
   The current API_BASE_ADDRESS is https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes
2. Start the service with `dotnet restore` and `dotnet run`
3. Navigate to the localhost address.
   Add stock symbols by intering a user name and a valid stock symbol and pressing the send button.

## Testing

Frontend tests are run with `npm test` in the ClientApp folder.

Backend tests are run with `dotnet test`
