using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaphyreProject;
using Moq;
using System.Collections.Generic;
using System.Threading;
using SaphyreProject.Models;

namespace SaphyreProjectTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _responseMessage;
        public MockHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(_responseMessage);
        }
    }

    [TestClass]
    public class StockQuoteClientTests
    {

        [TestMethod]
        public async Task BadRequestReturnsNull()
        {
            Environment.SetEnvironmentVariable("API_KEY", "123");
            Environment.SetEnvironmentVariable("API_HOST", "www.host");
            Environment.SetEnvironmentVariable("API_BASE_ADDRESS", "http://www.xyz.com");

            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var httpClient = new HttpClient(new MockHttpMessageHandler(responseMessage));

            var stockClient = new StockQuoteClient(httpClient);

            var stockList = new List<string>();
            stockList.Add("AAPL");
            var result = await stockClient.GetQuotes(stockList);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task SuccessfulRequestReturnsListofStocks()
        {
            Environment.SetEnvironmentVariable("API_KEY", "123");
            Environment.SetEnvironmentVariable("API_HOST", "www.host");
            Environment.SetEnvironmentVariable("API_BASE_ADDRESS", "http://www.xyz.com");

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"quoteResponse\":{\"result\":[{\"symbol\":\"AAPL\",\"bid\":100}] }}")
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(responseMessage));

            var stockClient = new StockQuoteClient(httpClient);

            var stockList = new List<string>();
            stockList.Add("AAPL");
            var actual = await stockClient.GetQuotes(stockList);

            var expectedStock = new Stock();
            expectedStock.Symbol = "AAPL";
            expectedStock.Price = 100;

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expectedStock.Price, actual[0].Price);
            Assert.AreEqual(expectedStock.Symbol, actual[0].Symbol);
        }
    }
}
ß