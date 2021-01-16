# SignalR Stockticker

This is an mvp for a Stockticker service that sends users price updates on stocks they choose.
It uses the Yahoo Finance API to get realtime stock information.
It is built with ASP.NET Core, SignalR, and React

## Running the app
This app requires credentials for the YahooFinance Api, available through RapidApi https://rapidapi.com/apidojo/api/yahoo-finance1
1. Set the environment variables APIKEY and APIHOST to the api credentials
2. dotnet run
3. Navigate to localhost:5000. Add stock symbols by intering a user name and a valid stock symbol and pressing the send button.
 


