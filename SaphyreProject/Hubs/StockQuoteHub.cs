using Microsoft.AspNetCore.SignalR;
using SaphyreProject.Models;
using System.Collections.Generic;

namespace SaphyreProject.Hubs
{
    public class StockQuoteHub : Hub
    {

        private readonly IStockTicker _stockTicker;

        public StockQuoteHub(IStockTicker stockTicker)
        {
            _stockTicker = stockTicker;
        }

        public string AddStock(string user, string symbol)
        {
            string message;
            if (user == null || user == string.Empty)
            {
                message = "No user provided!";
            }
            else if (symbol == null || symbol == string.Empty)
            {
                message = "No stock symbol provided!";
            }
            else
            {
                symbol = symbol.ToUpper();
                var stock = _stockTicker.TryAddStock(user, symbol);
                message = stock == null ? $"Error: Could not add {symbol}" : $"{symbol} added to your stock list";
            }

            return message;
        }

        public IList<Stock> GetMyStocks(string user)
        {
            if (user == null || user == string.Empty)
            {
                return null;

            }
            var usersStocks = _stockTicker.GetStocksByUser(user);
            return usersStocks;
        }
    }
}
