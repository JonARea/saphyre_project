using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using SaphyreProject.Hubs;
using SaphyreProject.Models;

namespace SaphyreProject
{
    public class StockTicker : IStockTicker
    {
        private readonly IHubContext<StockQuoteHub> _hubContext;

        private readonly IStockQuoteClient _quoteClient;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Stock>> _stocksByUser = new ConcurrentDictionary<string, ConcurrentDictionary<string, Stock>>();

        private readonly object _updateStockPricesLock = new object();

        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(10);

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;

        public StockTicker()
        {
            _quoteClient = new StockQuoteClient();
            _stocksByUser.Clear();

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }

        private IHubClients Clients
        {
            get;
            set;
        }

        public Stock TryAddStock(string user, string symbol)
        {
            var stocksBySymbol = _stocksByUser.GetOrAdd(user, (user) => new ConcurrentDictionary<string, Stock>());
            var stock = new Stock
            {
                Symbol = symbol,
                Price = 0
            };
            return stocksBySymbol.GetOrAdd(symbol, (symbol) => stock);
        }

        public IList<Stock> GetStocksByUser(string user)
        {

            if (_stocksByUser.TryGetValue(user, out var usersStocks))
            {
                return usersStocks.Values.ToList();
            }
            return null;
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    //if (TryUpdateStockPrices(_stocks.Values))
                    //{
                    //    BroadcastStockPrice(stock);
                    //}

                    //_updatingStockPrices = false;
                }
            }
        }

    }
}