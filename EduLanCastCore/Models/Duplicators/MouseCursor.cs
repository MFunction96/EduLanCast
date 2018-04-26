using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Models.Duplicators
{
    /// <summary>
    /// Draws the MouseCursor on an Image
    /// </summary>
    public static class MouseCursor
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct IconInfo
        {
            private readonly bool fIcon;
            public readonly int xHotspot;
            public readonly int yHotspot;
            public readonly IntPtr hbmMask;
            public readonly IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CursorInfo
        {
            public int cbSize;
            public readonly int flags;
            public readonly IntPtr hCursor;
            public Point ptScreenPos;
        }

        #region PInvoke

        private const string DllName = "user32.dll";

        [DllImport(DllName)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport(DllName)]
        private static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport(DllName)]
        private static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport(DllName)]
        private static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

        [DllImport(DllName)]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        #endregion

        private const int CursorShowing = 1;
                
        /// <summary>
        /// Gets the Current Mouse Cursor Position.
        /// </summary>
        public static Point CursorPosition
        {
            get
            {
                var p = new Point();
                GetCursorPos(ref p);
                return p;
            }
        }

        // hCursor -> (Icon, Hotspot)
        static readonly Dictionary<IntPtr, Tuple<Bitmap, Point>> Cursors = new Dictionary<IntPtr, Tuple<Bitmap, Point>>();
        
        /// <summary>
        /// Draws this overlay.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object to draw upon.</param>
        /// <param name="transform">Point Transform Function.</param>
        public static void Draw(Graphics g, Func<Point, Point> transform = null)
        {
            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InlineOutVariableDeclaration
            var cursorInfo = new CursorInfo { cbSize = Marshal.SizeOf<CursorInfo>() };

            if (!GetCursorInfo(out cursorInfo))
                return;

            if (cursorInfo.flags != CursorShowing)
                return;

            Bitmap icon;
            Point hotspot;

            if (Cursors.ContainsKey(cursorInfo.hCursor))
            {
                var tuple = Cursors[cursorInfo.hCursor];

                icon = tuple.Item1;
                hotspot = tuple.Item2;
            }
            else
            {
                var hIcon = CopyIcon(cursorInfo.hCursor);

                if (hIcon == IntPtr.Zero)
                    return;

                if (!GetIconInfo(hIcon, out var icInfo))
                    return;

                icon = Icon.FromHandle(hIcon).ToBitmap();
                hotspot = new Point(icInfo.xHotspot, icInfo.yHotspot);

                Cursors.Add(cursorInfo.hCursor, Tuple.Create(icon, hotspot));

                DestroyIcon(hIcon);

                DeleteObject(icInfo.hbmColor);
                DeleteObject(icInfo.hbmMask);
            }

            var location = new Point(cursorInfo.ptScreenPos.X - hotspot.X,
                cursorInfo.ptScreenPos.Y - hotspot.Y);

            if (transform != null)
                location = transform(location);

            try
            {
                g.DrawImage(icon, new Rectangle(location, icon.Size));
            }
            catch (ArgumentException) { }
        }
    }
}