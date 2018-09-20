using EduLanCastCore.Services.Enums;
using Microsoft.Win32;
using System;

namespace EduLanCastCore.Models.Registries
{
    /// <inheritdoc cref="RegKey" />
    /// <summary>
    /// 注册表信息存储类。
    /// </summary>
    [Serializable]
    public class RegStore : RegKey
    {
        /// <summary>
        /// 注册表信息是否为空。
        /// </summary>
        public bool IsNull { get; protected set; }
        /// <summary>
        /// 注册表信息是否必要。
        /// </summary>
        public bool IsNecessary { get; protected set; }
        /// <inheritdoc />
        /// <summary>
        /// 注册表信息存储类序列化构造函数。
        /// </summary>
        public RegStore() { }
        /// <inheritdoc />
        /// <summary>
        /// 注册表信息存储类构造函数。
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
        /// <param name="lpKind">
        /// 注册表键值类型。
        /// </param>
        /// <param name="lpValue">
        /// 注册表键值。
        /// </param>
        /// <param name="isNull">
        /// 注册表信息是否为空。
        /// </param>
        /// <param name="isNecessary">
        /// 注册表信息是否必要。
        /// </param>
        public RegStore(
            REG_ROOT_KEY hKey,
            string lpSubKey,
            string lpValueName = "",
            RegistryValueKind lpKind = RegistryValueKind.Unknown,
            object lpValue = null,
            bool isNull = true,
            bool isNecessary = true) :
            base(hKey, lpSubKey, lpValueName, lpKind, lpValue)
        {
            IsNull = isNull;
            IsNecessary = isNecessary;
        }

        /// <inheritdoc />
        /// <summary>
        /// 注册表信息存储类构造函数。
        /// </summary>
        /// <param name="regPath">
        /// 注册表键路径信息类。
        /// </param>
        /// <param name="lpKind">
        /// 注册表键值类型。
        /// </param>
        /// <param name="lpValue">
        /// 注册表键值。
        /// </param>
        /// <param name="isNull">
        /// 注册表信息是否为空。
        /// </param>
        /// <param name="isNecessary">
        /// 注册表信息是否必要。
        /// </param>
        public RegStore(
            RegPath regPath,
            RegistryValueKind lpKind = RegistryValueKind.Unknown,
            object lpValue = null,
            bool isNull = true,
            bool isNecessary = true) :
            base(regPath, lpKind, lpValue)
        {
            IsNull = isNull;
            IsNecessary = isNecessary;
        }

        /// <inheritdoc />
        /// <summary>
        /// 注册表信息存储类构造函数。
        /// </summary>
        /// <param name="regKey">
        /// 注册表键信息类。
        /// </param>
        /// <param name="isNull">
        /// 注册表信息是否为空。
        /// </param>
        /// <param name="isNecessary">
        /// 注册表信息是否必要。
        /// </param>
        public RegStore(RegKey regKey, bool isNull = true, bool isNecessary = true) :
            base(regKey)
        {
            IsNull = isNull;
            IsNecessary = isNecessary;
        }
        /// <inheritdoc />
        /// <summary>
        /// 注册表信息存储类复制构造函数。
        /// </summary>
        /// <param name="regStore">
        /// 注册表信息存储类。
        /// </param>
        public RegStore(RegStore regStore) :
            base(regStore.HKey, regStore.LpSubKey, regStore.LpValueName, regStore.LpKind, regStore.LpValue)
        {
            IsNull = regStore.IsNull;
            IsNecessary = regStore.IsNecessary;
        }
        /// <summary>
        /// 获取注册表键信息。
        /// </summary>
        /// <returns>
        /// 注册表键信息类。
        /// </returns>
        public RegKey GetRegKey()
        {
            return new RegKey(GetRegPath(), LpKind, LpValue);
        }
        /// <summary>
        /// 获取当前对象的深表副本。
        /// </summary>
        /// <returns>
        /// 当前对象的深表副本。
        /// </returns>
        public new object Clone()
        {
            return MemberwiseClone();
        }
        /// <summary>
        /// 注册表路径信息类默认排序规则。
        /// </summary>
        /// <param name="obj">
        /// 待比较的对象。
        /// </param>
        /// <returns>
        /// 大小比较结果。
        /// </returns>
        public new int CompareTo(object obj)
        {
            if (!(obj is RegStore regkey)) throw new NullReferenceException();
            var flag = base.CompareTo(obj);
            if (flag != 0) return flag;
            if (IsNull ^ regkey.IsNull) return 1;
            return IsNecessary ^ regkey.IsNecessary ? 1 : -1;
        }
    }
}
