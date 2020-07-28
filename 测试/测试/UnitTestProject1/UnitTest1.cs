using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 测试;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 c=new Class1();
            Assert.AreEqual(c.getAdd(1, 2),3);
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            Class1 c = new Class1();
            Assert.AreEqual(c.getAdd(1, 2), 3);

        }
    }
}
