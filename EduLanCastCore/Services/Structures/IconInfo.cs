using System;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Services.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IconInfo
    {
        private readonly bool fIcon;
        public readonly int xHotspot;
        public readonly int yHotspot;
        public readonly IntPtr hbmMask;
        public readonly IntPtr hbmColor;
    }
}
