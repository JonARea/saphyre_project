using System;
using System.Collections.Generic;

namespace SaphyreProject.Models
{
    public interface IStockTicker
    {
        public Stock TryAddStock(string user, string symbol);

        public IList<Stock> GetStocksByUser(string user);
    }
}
