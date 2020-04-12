using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinancialChat;
using FinancialChat.Controllers;
using FinancialChat.Models.Bot;

namespace FinancialChat.Tests.Controllers
{
    [TestClass]
    public class BotTest
    {
        private IBot _bot { get; }

        public BotTest()
        {
            _bot = new Bot();
        }

        [TestMethod]
        public void WrongCodeTest()
        {
            // Arrange
            string message = "/stock=stock_code";

            // Act
            var result = _bot.GetQuote(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("it's not valid"));
        }

        [TestMethod]
        public void NostockParamerDetectedTest()
        {
            // Arrange
            string message = "stock=stock_code";

            // Act
            var result = _bot.GetQuote(message);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StockParamerDetectedAndValidCodeTest()
        {
            // Arrange
            string message = "/stock=AAPL.US";

            // Act
            var result = _bot.GetQuote(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("$"));
        }

    }
}
