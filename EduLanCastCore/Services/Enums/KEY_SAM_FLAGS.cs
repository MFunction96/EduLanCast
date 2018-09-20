// ReSharper disable InconsistentNaming

using System;

namespace EduLanCastCore.Services.Enums
{
    /// <summary>
    /// 注册表访问位宽。
    /// </summary>
    [Flags]
    public enum KEY_SAM_FLAGS
    {
        KEY_WOW64_64KEY = 0x100,
        KEY_WOW64_32KEY = 0x200
    }
}
