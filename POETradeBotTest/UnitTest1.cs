using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POETradeBot;

namespace POETradeBotTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Reader reader = new Reader();
            var thing = reader.CheckImage(@"D:\code\POETradeBot\POETradeBotTest\testdata\test1.PNG");
            var output = "køre til rødovre modis gang";
            Assert.Equals(output, thing);

        }
    }
}
