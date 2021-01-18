using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using SaphyreProject.Models;

namespace SaphyreProject
{
    public class StockTicker : IStockTicker
    {

        private readonly IStockQuoteClient _quoteClient;

        private readonly ConcurrentDictionary<string, HashSet<string>> _stocksByUser = new ConcurrentDictionary<string, HashSet<string>>();

        private readonly ConcurrentDictionary<string, Stock> _allStocks = new ConcurrentDictionary<string, Stock>();

        private readonly SemaphoreSlim _updateStockPricesLock = new SemaphoreSlim(1, 1);

        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(20);

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;

        public StockTicker()
        {
            _quoteClient = new StockQuoteClient(new HttpClient());
            _stocksByUser.Clear();

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }

        public Stock TryAddStock(string user, string symbol)
        {

            symbol = symbol.ToUpper();
            var stockSet = _stocksByUser.GetOrAdd(user, (user) => new HashSet<string>());

            stockSet.Add(symbol);

            if (_allStocks.TryGetValue(symbol, out var stock))
            {
                return stock;
            }
            else
            {
                stock = new Stock();
                stock.Symbol = symbol;
                stock.Price = 0;
                _allStocks.TryAdd(symbol, stock);
                return stock;
            }

        }

        public IList<Stock> GetStocksByUser(string user)
        {

            if (_stocksByUser.TryGetValue(user, out var usersStocks))
            {
                return usersStocks.Select(symbol =>
                {
                    if (_allStocks.TryGetValue(symbol, out var stock))
                    {
                        return stock;
                    }
                    else
                    {
                        return new Stock();
                    }
                }).ToList();
            }
            return null;
        }

        private async void UpdateStockPrices(object state)
        {

            await _updateStockPricesLock.WaitAsync();

            var stocksToUpdate = _allStocks.Keys.ToList();

            try
            {
                if (!_updatingStockPrices && stocksToUpdate.Count > 0)
                {
                    _updatingStockPrices = true;

                    var stocks = await _quoteClient.GetQuotes(stocksToUpdate);


                    foreach (var stock in stocks)
                    {
                        _allStocks.AddOrUpdate(stock.Symbol, stock, (key, oldValue) => stock);
                    }
                }
                _updatingStockPrices = false;
            }
            finally
            {
                _updateStockPricesLock.Release();
            }
        }
    }
}