using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaphyreProject.Models;
using SaphyreProject.Hubs;
using System.Collections.Generic;

namespace SaphyreProjectTests
{
    [TestClass]
    public class StockQuoteHubTests
    {
        [TestMethod]
        public void AddStockBadInput()
        {
            var stockTicker = new Mock<IStockTicker>();
            stockTicker.Setup(st => st.TryAddStock(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Stock());

            var hub = new StockQuoteHub(stockTicker.Object);

            var result = hub.AddStock("", "msft");

            stockTicker.VerifyNoOtherCalls();
            Assert.AreEqual("No user provided!", result);

            result = hub.AddStock("jon", "");

            stockTicker.VerifyNoOtherCalls();
            Assert.AreEqual("No stock symbol provided!", result);
        }

        [TestMethod]
        public void AddStockSuccess()
        {
            var stockTicker = new Mock<IStockTicker>();
            stockTicker.Setup(st => st.TryAddStock(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Stock());

            var hub = new StockQuoteHub(stockTicker.Object);

            var result = hub.AddStock("jon", "msft");

            stockTicker.VerifyAll();
            Assert.AreEqual("MSFT added to your stock list", result);

        }

        [TestMethod]
        public void AddStockFailure()
        {
            var stockTicker = new Mock<IStockTicker>();
            stockTicker.Setup(st => st.TryAddStock(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<Stock>(null);

            var hub = new StockQuoteHub(stockTicker.Object);

            var result = hub.AddStock("jon", "blah");

            stockTicker.VerifyAll();
            Assert.AreEqual("Error: Could not add BLAH", result);
        }

        [TestMethod]
        public void GetMyStocksSuccess()
        {
            var stockTicker = new Mock<IStockTicker>();
            stockTicker.Setup(st => st.GetStocksByUser(It.IsAny<string>()))
                .Returns(new List<Stock>() { new Stock() });

            var hub = new StockQuoteHub(stockTicker.Object);

            var result = hub.GetMyStocks("jon");

            stockTicker.VerifyAll();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetMyStocksBadInput()
        {
            var stockTicker = new Mock<IStockTicker>();
            stockTicker.Setup(st => st.GetStocksByUser(It.IsAny<string>()))
                .Returns(new List<Stock>() { new Stock() });

            var hub = new StockQuoteHub(stockTicker.Object);

            var result = hub.GetMyStocks("");

            stockTicker.VerifyNoOtherCalls();
            Assert.IsNull(result);
        }
    }
}
