using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
}
