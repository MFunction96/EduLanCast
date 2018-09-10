using EduLanCastCore.Models.Configs;
using System;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Utils
{
    public static class ErrorUtil
    {
        public static async Task WriteError(Exception e)
        {
            var path = $"{AppConfig.ConfigPath}\\{AppConfig.ErrorName}";
            await FileUtil.ExportObj(e, path, true);
        }
    }
}
