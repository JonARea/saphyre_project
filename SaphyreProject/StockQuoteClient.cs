using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SaphyreProject.Models;
using System.Text.Json;
using System.Linq;

namespace SaphyreProject
{
    public class StockQuoteClient : IStockQuoteClient
    {
        private readonly HttpClient client;
        private readonly string apiKey = Environment.GetEnvironmentVariable("APIKEY");
        private readonly string apiHost = Environment.GetEnvironmentVariable("APIHOST");

        public StockQuoteClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?region=US");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<object> GetQuotes(IList<string> symbols)
        {
            var symbolParams = new Uri("&symbols=" + String.Join(',', symbols));

            if (!Uri.TryCreate(client.BaseAddress, symbolParams, out Uri fullUri))
            {
                Console.WriteLine("Invalid input");
                return null;
            }

            if (!client.BaseAddress.IsBaseOf(fullUri))
            {
                Console.WriteLine("Invalid input, fullUri is not a base of the BaseAddress");
                return null;
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = fullUri,
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            try
            {
                return await response.Content.ReadFromJsonAsync<object>();

            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON");
            }
            return null;
        }
    }
}
