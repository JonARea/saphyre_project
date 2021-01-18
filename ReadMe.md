# SignalR Stockticker

This is an mvp for a Stockticker service that sends users price updates on stocks they choose.
It uses the Yahoo Finance API to get realtime stock information.
It is built with ASP.NET Core, SignalR, and React

## Running the app
This app requires credentials for the YahooFinance Api, available through RapidApi https://rapidapi.com/apidojo/api/yahoo-finance1
1. Set the environment variables API_KEY, API_HOST, and API_BASE_ADDRESS to the api credentials and url
2. dotnet run
3. Navigate to https://localhost:5001. Add stock symbols by intering a user name and a valid stock symbol and pressing the send button.
 
