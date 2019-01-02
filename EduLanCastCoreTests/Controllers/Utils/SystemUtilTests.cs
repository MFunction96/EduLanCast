using System;
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
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void BlockInputTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetBiosSerialTest()
        {
            Console.Out.WriteLine(SystemUtil.GetBiosSerial());
        }
    }
}