using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Models.Configs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace EduLanCastCoreTests.Controllers.Threads
{
    [TestClass()]
    public class PresentThreadTests
    {
        [TestMethod()]
        public void BlockTest()
        {
            var config = new AppConfig
            {
                AllowInput = false,
                Fps = 10
            };
            var present = new PresentThread(ref config);
            present.Start();
            Thread.Sleep(10000);
            present.Interrupt();
        }

        [TestMethod()]
        public void PresentThreadTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void StartTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void OperationTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void InterruptTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void DisposeTest()
        {
            //Assert.Fail();
        }
    }
}