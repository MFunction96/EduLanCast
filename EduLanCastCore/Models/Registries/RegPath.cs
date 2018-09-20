using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EduLanCastCore.Models.Registries
{
    /// <inheritdoc cref="IComparable" />
    /// <summary>
    /// 注册表路径信息类。
    /// </summary>
    [Serializable]
    public class RegPath : ICloneable, IComparable
    {
        #region Properties

        /// <summary>
        /// 全球唯一标识符。
        /// </summary>
        public Guid Guid { get; }
        /// <summary>
        /// 注册表根键。
        /// </summary>
        public REG_ROOT_KEY HKey { get; protected set; }
        /// <summary>
        /// 注册表子键。
        /// </summary>
        public string LpSubKey { get; protected set; }
        /// <summary>
        /// 注册表键名。
        /// </summary>
        public string LpValueName { get; protected set; }

        #endregion

        #region Construction

        /// <summary>
        /// 注册表路径信息类序列化构造函数。
        /// </summary>
        public RegPath()
        {
            Guid = Guid.NewGuid();
        }
        /// <summary>
        /// 注册表路径信息类构造函数。
        /// </summary>
        /// <param name="path">
        /// 注册表路径信息。
        /// </param>
        /// <param name="refMark">
        /// 是否为字符串引用。
        /// </param>
        public RegPath(string path, bool refMark = false)
        {
            Guid = Guid.NewGuid();
            if (refMark) path = path.Substring(1, path.Length - 2);
            var index1 = path.IndexOf('\\');
            var index2 = path.LastIndexOf('\\');
            var tmp = path.Substring(0, index1);
            switch (tmp)
            {
                case @"HKEY_CLASSES_ROOT":
                    HKey = REG_ROOT_KEY.HKEY_CLASSES_ROOT;
                    break;
                case @"HKEY_CURRENT_USER":
                    HKey = REG_ROOT_KEY.HKEY_CURRENT_CONFIG;
                    break;
                case @"HKEY_LOCAL_MACHINE":
                    HKey = REG_ROOT_KEY.HKEY_LOCAL_MACHINE;
                    break;
                case @"HKEY_USERS":
                    HKey = REG_ROOT_KEY.HKEY_USERS;
                    break;
                case @"HKEY_PERFORMANCE_DATA":
                    HKey = REG_ROOT_KEY.HKEY_PERFORMANCE_DATA;
                    break;
                case @"HKEY_CURRENT_CONFIG":
                    HKey = REG_ROOT_KEY.HKEY_CURRENT_CONFIG;
                    break;
                default:
                    HKey = REG_ROOT_KEY.HKEY_DYN_DATA;
                    break;
            }
            LpSubKey = path.Substring(index1 + 1, index2 - index1 - 1);
            LpValueName = path.Substring(index2 + 1, path.Length - index2 - 1);
        }
        /// <summary>
        /// 注册表路径信息类构造函数。
        /// </summary>
        /// <param name="hKey">
        /// 注册表根键。
        /// </param>
        /// <param name="lpSubKey">
        /// 注册表子键。
        /// </param>
        /// <param name="lpValueName">
        /// 注册表键名。
        /// </param>
        public RegPath(REG_ROOT_KEY hKey, string lpSubKey, string lpValueName = "")
        {
            Guid = Guid.NewGuid();
            HKey = hKey;
            LpSubKey = lpSubKey;
            LpValueName = lpValueName;
        }
        /// <summary>
        /// 注册表路径信息类复制构造函数。
        /// </summary>
        /// <param name="regPath">
        /// 注册表路径信息类。
        /// </param>
        public RegPath(RegPath regPath)
        {
            Guid = Guid.NewGuid();
            HKey = regPath.HKey;
            LpSubKey = regPath.LpSubKey;
            LpValueName = regPath.LpValueName;
        }

        #endregion

        #region Methods

        #region Shared

        public IEnumerable<RegPath> EnumKey()
        {
            var phkresult = RegOpenKey(this);
            var list = new List<RegPath>();
            for (var index = 0; ; index++)
            {
                var sb = new StringBuilder(0x7FFF);
                var size = 0x7FFF;
                var regenumkeytmp = NativeMethods.RegEnumKeyEx(phkresult, index, sb, ref size, IntPtr.Zero, IntPtr.Zero,
                    IntPtr.Zero, out _);
                if (regenumkeytmp == (int)ERROR_CODE.ERROR_NO_MORE_ITEMS)
                {
                    break;
                }
                if (regenumkeytmp != (int)ERROR_CODE.ERROR_SUCCESS)
                {
                    throw new Exception(@"注册表键值枚举失败" + '\n' + regenumkeytmp + '\n' + nameof(EnumKey));
                }
                list.Add(new RegPath(HKey, LpSubKey + '\\' + sb));
            }
            NativeMethods.RegCloseKey(phkresult);

            list.Sort((t1, t2) => string.CompareOrdinal(t1.LpValueName, t2.LpValueName));

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RegKey QueryValue()
        {
            RegKey regkey;
            try
            {
                var phkresult = RegOpenKey(this);
                var lpcbData = 0;
                NativeMethods.RegQueryValueEx(phkresult, LpValueName, IntPtr.Zero, out var lpkind, IntPtr.Zero, ref lpcbData);
                if (lpcbData == 0)
                {
                    NativeMethods.RegCloseKey(phkresult);
                    throw new Exception(@"注册表访问失败" + '\n' + @"无法获取缓冲区大小" + '\n' + nameof(QueryValue));
                }
                var lpdata = Marshal.AllocHGlobal(lpcbData);
                var reggetvaluetemp = NativeMethods.RegQueryValueEx(phkresult, LpValueName, IntPtr.Zero, out lpkind, lpdata, ref lpcbData);
                if (reggetvaluetemp != (int)ERROR_CODE.ERROR_SUCCESS)
                {
                    throw new Exception(@"注册表访问失败" + '\n' + reggetvaluetemp + '\n' + nameof(QueryValue));
                }
                NativeMethods.RegCloseKey(phkresult);
                if (reggetvaluetemp != (int)ERROR_CODE.ERROR_SUCCESS)
                {
                    throw new Exception(@"注册表访问失败" + '\n' + reggetvaluetemp + '\n' + nameof(QueryValue));
                }
                regkey = ConvertData(this, lpkind, lpdata, lpcbData);
            }
            catch (Exception)
            {
                regkey = new RegKey(this);
            }
            return regkey;
        }

        #endregion

        #region Implement

        /// <inheritdoc />
        /// <summary>
        /// 获取当前对象的深表副本。
        /// </summary>
        /// <returns>
        /// 当前对象的深表副本。
        /// </returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
        /// <inheritdoc />
        /// <summary>
        /// 注册表路径信息类默认排序规则。
        /// </summary>
        /// <param name="obj">
        /// 待比较的对象。
        /// </param>
        /// <returns>
        /// 大小比较结果。
        /// </returns>
        public int CompareTo(object obj)
        {
            if (!(obj is RegPath regpath)) throw new NullReferenceException();
            if (HKey < regpath.HKey) return 1;
            if (HKey > regpath.HKey) return -1;
            var flag = string.CompareOrdinal(LpSubKey, regpath.LpSubKey);
            return flag != 0 ? flag : string.CompareOrdinal(LpValueName, regpath.LpValueName);
        }
        #endregion

        #region Private

        /// <summary>
        /// 打开注册表子键句柄
        /// </summary>
        /// <param name="regPath">
        /// 待打开的注册表键信息
        /// </param>
        /// <returns>
        /// 注册表子键句柄
        /// </returns>
        private static IntPtr RegOpenKey(RegPath regPath)
        {
            int regopenkeytmp;
            IntPtr phkresult;
            if (Environment.Is64BitOperatingSystem)
            {
                regopenkeytmp = NativeMethods.RegOpenKeyEx(new IntPtr((int)regPath.HKey), regPath.LpSubKey, 0,
                    (int)KEY_SAM_FLAGS.KEY_WOW64_64KEY |
                    (int)KEY_ACCESS_TYPE.KEY_READ, out phkresult);
            }
            else
            {
                regopenkeytmp = NativeMethods.RegOpenKeyEx(new IntPtr((int)regPath.HKey), regPath.LpSubKey, 0,
                    (int)KEY_ACCESS_TYPE.KEY_READ, out phkresult);
            }
            if (regopenkeytmp == (int)ERROR_CODE.ERROR_FILE_NOT_FOUND)
            {
                throw new NullReferenceException(@"注册表访问失败" + '\n' + regopenkeytmp + '\n' + nameof(RegOpenKey));
            }
            if (regopenkeytmp != (int)ERROR_CODE.ERROR_SUCCESS)
            {
                throw new Exception(@"注册表访问失败" + '\n' + regopenkeytmp + '\n' + nameof(RegOpenKey));
            }
            return phkresult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regPath"></param>
        /// <param name="lpKind"></param>
        /// <param name="lpData"></param>
        /// <param name="lpcbData"></param>
        /// <returns></returns>
        private static RegKey ConvertData(RegPath regPath, RegistryValueKind lpKind, IntPtr lpData, int lpcbData)
        {
            RegKey regkey;
            if (lpKind == RegistryValueKind.DWord)
            {
                var lpdataint = Marshal.ReadInt32(lpData);
                regkey = new RegKey(regPath, lpKind, lpdataint);
            }
            else if (lpKind == RegistryValueKind.QWord)
            {
                var lpdataint = Marshal.ReadInt64(lpData);
                regkey = new RegKey(regPath, lpKind, lpdataint);
            }
            else if (lpKind == RegistryValueKind.String ||
                     lpKind == RegistryValueKind.ExpandString ||
                     lpKind == RegistryValueKind.MultiString)
            {
                var lpdatastr = Marshal.PtrToStringUni(lpData);
                lpdatastr = lpdatastr?.Trim();
                regkey = new RegKey(regPath, lpKind, lpdatastr);
            }
            else if (lpKind == RegistryValueKind.Binary)
            {
                var lpdatabin = new byte[lpcbData];
                Marshal.Copy(lpData, lpdatabin, 0, lpcbData);
                regkey = new RegKey(regPath, lpKind, lpdatabin);
            }
            else
            {
                throw new Exception(@"注册表访问失败" + '\n' + @"注册表数据类型异常" + '\n' + nameof(ConvertData));
            }
            return regkey;
        }

        #endregion

        #endregion
    }
}