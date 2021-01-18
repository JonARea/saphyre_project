using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaphyreProject.Models
{
    public interface IStockQuoteClient
    {
        public Task<IList<Stock>> GetQuotes(IList<string> symbols);
    }
}
