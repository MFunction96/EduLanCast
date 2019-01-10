using EduLanCastCore.Models.Configs;
using System;

namespace EduLanCastCore.Controllers.Utils
{
    public static class ErrorUtil
    {
        public static void WriteError(Exception e)
        {
            var path = $"{AppConfig.ConfigPath}\\{AppConfig.ErrorName}";
            FileUtil.ExportJson(e, path, true);
        }
    }
}
