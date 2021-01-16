using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaphyreProject.Models
{
    public interface IStockQuoteClient
    {
        public Task<object> GetQuotes(IList<string> symbols);
    }
}
