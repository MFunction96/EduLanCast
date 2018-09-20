using System;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Models.Configs
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AppConfig : IDisposable
    {
        public const string ConfigPath = @"";
        public const string ConfigName = @"";
        public const string ErrorName = @"";

        public int Fps { get; set; }

        public int BufferSize { get; set; }

        public bool AllowInput { get; set; }

        public AppConfig()
        {
            
        }

        public void Dispose()
        {
        }
    }
}
