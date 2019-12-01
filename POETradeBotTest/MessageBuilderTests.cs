using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POETestBot.Models;
using POETradeBot;

namespace POETradeBotTest
{
    [TestClass]
    public class MessageBuildTests
    {
        [TestMethod]
        public void PoeDotTrade1()
        {
            //Arrange
            var input = "From <Gif+1> Baroox: Hi, | would like to buy your Lion's Roar Granite Flask listed for 1 gcp in Standard (stash tab 83; position: left 5, top 4)";
            var expected = new ItemBuyDTO {
                username = "Baroox",
                itemname = "Lion's Roar Granite Flask",
                price = ("gcp",1),
                position = ("8",5,4)
            };

            //Act
            var output = MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal(expected, output);

        }
    }
}
