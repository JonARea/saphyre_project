using Microsoft.AspNetCore.SignalR;
using SaphyreProject.Models;
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

        public async Task AddStock(string user, string symbol)
        {
            var stock = _stockTicker.TryAddStock(user, symbol);
            var message = stock == null ? $"Could not add {symbol}" : $"{symbol} added";

            await Clients.Caller.SendAsync("AddStockResult", user, message);
        }

        public async Task GetMyStocks(string user)
        {
            if (user != null)
            {
                var usersStocks = _stockTicker.GetStocksByUser(user);
                await Clients.Caller.SendAsync("ReceiveStocks", usersStocks);
            }
        }

    }
}
