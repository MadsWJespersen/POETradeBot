using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POETradeBot;

namespace POETradeBotTest
{
    [TestClass]
    public class MessageBuildTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Reader reader = new Reader();
            MessageBuilder mb = new MessageBuilder();
            
            Assert.Equals(output, thing);

        }
    }
}
