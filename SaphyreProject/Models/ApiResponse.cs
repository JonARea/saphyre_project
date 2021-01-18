using System;
using System.Collections.Generic;

namespace SaphyreProject.Models
{
    public class ApiResponse
    {
        public IDictionary<string, IEnumerable<ApiQuote>> quoteResponse { get; set; }
    }

    public class ApiQuote
    {
        public decimal bid { get; set; }
        public string symbol { get; set; }
    }
}
