using System;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POETradeBot;
using Assert = Xunit.Assert;

namespace POETradeBotTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]    
        public void TestMethod1()
        {
            Reader reader = new Reader();
            var output = reader.CheckImage(@"D:\code\POETradeBot\POETradeBot\Capture.PNG");
            Assert.Equal("", output);
        }
    }
}
