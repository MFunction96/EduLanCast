using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
using System;
using System.Data.SqlTypes;
using System.Management;

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
            var comSerial = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            var result = string.Empty;
            foreach (var o in comSerial.Get())
            {
                try
                {
                    if (o is ManagementObject wmi)
                    {
                        result = wmi.GetPropertyValue("SerialNumber").ToString();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            if (result == string.Empty) throw new SqlNullValueException(nameof(GetBiosSerial));
            return result;
        }
    }
}
