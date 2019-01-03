using System;
using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Controllers.Utils
{
    public class SystemUtil
    {
        public static void KeepScreenOn(bool flag)
        {
            if (flag)
            {
                NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS |
                                                      EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                      EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            }
            else
            {
                NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
        }

        public static void BlockInput(bool flag)
        {
            NativeMethods.BlockInput(flag);
        }

        public static string GetBiosSerial()
        {
            throw new NotImplementedException();
            //var ptr = NativeMethods.GetBiosSerial();
            //return Marshal.PtrToStringBSTR(ptr);
        }
    }
}
