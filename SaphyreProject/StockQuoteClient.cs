using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SaphyreProject.Models;
using System.Text.Json;

namespace SaphyreProject
{
    public class StockQuoteClient : IStockQuoteClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY");
        private readonly string _apiHost = Environment.GetEnvironmentVariable("API_HOST");
        private readonly string _apiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS");

        public StockQuoteClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_apiBaseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private Uri GetRequestUri(IList<string> symbols)
        {
            var symbolParams = new Uri("?region=US" + "&symbols=" + String.Join(',', symbols), UriKind.Relative);

            if (!Uri.TryCreate(_client.BaseAddress, symbolParams, out Uri fullUri))
            {
                Console.WriteLine("Invalid input");
                return null;
            }

            if (!_client.BaseAddress.IsBaseOf(fullUri))
            {
                Console.WriteLine("Invalid input, fullUri is not a base of the BaseAddress");
                return null;
            }

            return fullUri;
        }

        public async Task<IList<Stock>> GetQuotes(IList<string> symbols)
        {
            var uri = GetRequestUri(symbols);

            if (uri == null)
            {
                return null;
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", _apiHost },
                },
            };

            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            try
            {

                var result = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<ApiResponse>(result);

                var output = new List<Stock>();
                if (deserialized.quoteResponse.TryGetValue("result", out IEnumerable<ApiQuote> quotes))
                {
                    foreach (var quote in quotes)
                    {
                        var stock = new Stock();
                        stock.Price = quote.bid;
                        stock.Symbol = quote.symbol;
                        output.Add(stock);
                    }
                }

                return output;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON");
            }
            return null;
        }
    }
}
