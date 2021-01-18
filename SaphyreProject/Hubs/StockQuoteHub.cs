using Microsoft.AspNetCore.SignalR;
using SaphyreProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaphyreProject.Hubs
{
    public class StockQuoteHub : Hub
    {

        private readonly StockTicker _stockTicker;

        public StockQuoteHub(StockTicker stockTicker)
        {
            _stockTicker = stockTicker;
        }

        public string AddStock(string user, string symbol)
        {
            var stock = _stockTicker.TryAddStock(user, symbol);
            var message = stock == null ? $"Could not add {symbol}" : $"{symbol} added";

            return message;
        }

        public IList<Stock> GetMyStocks(string user)
        {
            if (user != null)
            {
                var usersStocks = _stockTicker.GetStocksByUser(user);
                return usersStocks;
            }
            return null;
        }

    }
}
