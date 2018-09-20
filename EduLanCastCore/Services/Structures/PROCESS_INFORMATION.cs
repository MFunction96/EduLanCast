using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace EduLanCastCore.Services.Structures
{
    /// <summary>
    /// 进程信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int ProcessId;
        public int ThreadId;
    }
}
