using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Services.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        public int cbSize;
        public readonly int flags;
        public readonly IntPtr hCursor;
        public Point ptScreenPos;
    }
}
