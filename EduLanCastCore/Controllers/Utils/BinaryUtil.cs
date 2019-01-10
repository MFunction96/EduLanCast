using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace EduLanCastCore.Controllers.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class BinaryUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeObject(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="binary"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static TType DeserializeObject<TType>(byte[] binary, int startIndex = 0, int length = 0)
        {
            byte[] tmp;
            if (length == 0)
            {
                tmp = binary;
            }
            else
            {
                tmp = new byte[length];
                Array.Copy(binary, startIndex, tmp, 0, length);
            }
            using (var stream = new MemoryStream(tmp))
            {
                var formatter = new BinaryFormatter();
                return (TType)formatter.Deserialize(stream);
            }
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ComputeSHA1(object obj)
        {
            var binary = SerializeObject(obj);

            var sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            var hash = sha.ComputeHash(binary);

            var sb = new StringBuilder(hash.Length << 1);

            foreach (var b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
