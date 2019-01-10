using System;
using System.Threading;
using EduLanCastCore.Controllers.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduLanCastCoreTests.Controllers.Utils
{
    [TestClass()]
    public class SystemUtilTests
    {
        [TestMethod()]
        public void KeepScreenOnTest()
        {
            SystemUtil.KeepScreenOn(true);
            Thread.Sleep(10000);
            SystemUtil.KeepScreenOn(false);
        }

        [TestMethod()]
        public void BlockInputTest()
        {
            SystemUtil.BlockInput(true);
            Thread.Sleep(10000);
            SystemUtil.BlockInput(false);
        }

        [TestMethod()]
        public void GetBiosSerialTest()
        {
            Console.Out.WriteLine(SystemUtil.GetBiosSerial());
        }
    }
}