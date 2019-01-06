using System;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        /// <param name="append"></param>
        public static async Task ExportStream(object obj, string filePath, bool append = false)
        {
            var fileinfo = new FileInfo(filePath);
            if (fileinfo.Directory != null && !fileinfo.Directory.Exists) fileinfo.Directory?.Create();
            

            if (append)
            {
                using (var fs = new FileStream(filePath, FileMode.Append))
                {
                    var bin = BinaryUtil.SerializeObject(obj);
                    await fs.WriteAsync(bin, 0, bin.Length);
                }
            }
            else
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    var bin = BinaryUtil.SerializeObject(obj);
                    await fs.WriteAsync(bin, 0, bin.Length);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<byte[]> ImportStream(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        /// <param name="append"></param>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static void ExportJson(object obj, string filePath, bool append = false, string formatString = ",\r\n")
        {
            var fileinfo = new FileInfo(filePath);
            if (fileinfo.Directory != null && !fileinfo.Directory.Exists) fileinfo.Directory?.Create();

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            if (append)
            {
                if (File.Exists(filePath)) json = formatString + json;
                File.AppendAllText(filePath, json);
            }
            else File.WriteAllText(filePath, json);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ImportJson<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
