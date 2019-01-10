using EduLanCastCore.Controllers.Utils;
using System;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Models.Configs
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class AppConfig : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ConfigPath = @"C:\ProgramData\EduLanCast";
        /// <summary>
        /// 
        /// </summary>
        public const string ConfigName = @"config.json";
        /// <summary>
        /// 
        /// </summary>
        public const string ErrorName = @"error.log";
        /// <summary>
        /// 
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BufferSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AllowInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AppConfig()
        {
            Fps = 24;
            Port = 15661;
            BufferSize = 1 << 20;
            AllowInput = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public static void InitConfig()
        {
            FileUtil.ExportJson(new AppConfig(), $"{ConfigPath}\\{ConfigName}");
            FileUtil.ExportJson(new AppConfig(), $"{ConfigPath}\\{ConfigName}");
        }
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;

        }
        /// <summary>
        /// 
        /// </summary>
        ~AppConfig()
        {
            Dispose(false);
        }
    }
}
