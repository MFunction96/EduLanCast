using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
using System;
using System.Data.SqlTypes;
using System.Management;

namespace EduLanCastCore.Controllers.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        public static void BlockInput(bool flag)
        {
            NativeMethods.BlockInput(flag);
            //var result = NativeMethods.GetLastError();
            //if (result != (int) ERROR_CODE.ERROR_SUCCESS) throw new SystemException($"Error Code : {result}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetBiosSerial()
        {
            var comSerial = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            var result = string.Empty;
            foreach (var o in comSerial.Get())
            {
                try
                {
                    if (!(o is ManagementObject wmi)) continue;
                    result = wmi.GetPropertyValue("SerialNumber").ToString();
                    if (result != string.Empty) break;
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
