using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Services
{
    public class NativeMethods
    {
        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct IconInfo
        {
            private readonly bool fIcon;
            public readonly int xHotspot;
            public readonly int yHotspot;
            public readonly IntPtr hbmMask;
            public readonly IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public int cbSize;
            public readonly int flags;
            public readonly IntPtr hCursor;
            public Point ptScreenPos;
        }

        #endregion

        #region PInvoke

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        #endregion
    }
}
