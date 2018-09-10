using System;

namespace EduLanCastCore.Models.Configs
{
    public class AppConfig : IDisposable
    {
        public const string ConfigPath = @"";
        public const string ConfigName = @"";
        public const string ErrorName = @"";

        public int BufferSize { get; set; }

        public AppConfig()
        {
            
        }

        public void Dispose()
        {
        }
    }
}
