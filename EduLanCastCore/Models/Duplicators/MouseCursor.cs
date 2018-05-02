using EduLanCastCore.Services;
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
        private const int CursorShowing = 1;
                
        /// <summary>
        /// Gets the Current Mouse Cursor Position.
        /// </summary>
        public static Point CursorPosition
        {
            get
            {
                var p = new Point();
                NativeMethods.GetCursorPos(ref p);
                return p;
            }
        }

        // hCursor -> (Icon, Hotspot)
        private static readonly Dictionary<IntPtr, Tuple<Bitmap, Point>> Cursors = new Dictionary<IntPtr, Tuple<Bitmap, Point>>();
        
        /// <summary>
        /// Draws this overlay.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> object to draw upon.</param>
        /// <param name="transform">Point Transform Function.</param>
        public static void Draw(Graphics g, Func<Point, Point> transform = null)
        {
            // ReSharper disable once RedundantAssignment
            // ReSharper disable once InlineOutVariableDeclaration
            var cursorInfo = new NativeMethods.CursorInfo { cbSize = Marshal.SizeOf<NativeMethods.CursorInfo>() };

            if (!NativeMethods.GetCursorInfo(out cursorInfo))
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
                var hIcon = NativeMethods.CopyIcon(cursorInfo.hCursor);

                if (hIcon == IntPtr.Zero)
                    return;

                if (!NativeMethods.GetIconInfo(hIcon, out var icInfo))
                    return;

                icon = Icon.FromHandle(hIcon).ToBitmap();
                hotspot = new Point(icInfo.xHotspot, icInfo.yHotspot);

                Cursors.Add(cursorInfo.hCursor, Tuple.Create(icon, hotspot));

                NativeMethods.DestroyIcon(hIcon);

                NativeMethods.DeleteObject(icInfo.hbmColor);
                NativeMethods.DeleteObject(icInfo.hbmMask);
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