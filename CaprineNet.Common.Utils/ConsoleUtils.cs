#region License
/*
    CaprineNet.Common.Utils
    Copyright(C) 2021 Caprine Logic
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.
    
    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
*/
#endregion License

using System;
using System.Runtime.InteropServices;

namespace CaprineNet.Common.Utils
{
    public static class ConsoleUtils
    {
        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        private const uint FLASHW_STOP = 0;
        private const uint FLASHW_CAPTION = 1;
        private const uint FLASHW_TRAY = 2;
        private const uint FLASHW_ALL = 3;
        private const uint FLASHW_TIMER = 4;
        private const uint FLASHW_TIMERNOFG = 12;

        private const uint SW_HIDE = 0;
        private const uint SW_SHOWNORMAL = 1;
        private const uint SW_MAXIMIZE = 3;
        private const uint SW_MINIMIZE = 6;
        private const uint SW_FORCEMINIMIZE = 11;

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;
        private const int WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public int dwTimeout;
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] uint nCmdShow);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        public static void Free() => FreeConsole();

        public static void DisableMaximize()
        {
            var val = GetWindowLong(GetConsoleWindow(), GWL_STYLE);
            SetWindowLong(GetConsoleWindow(), GWL_STYLE, val & ~WS_MAXIMIZEBOX);
        }

        public static void SetFixedSize(int width, int height, bool disableMinimize, bool disableMaximize)
        {
            IntPtr console = GetConsoleWindow();

            Console.WindowWidth = width;
            Console.WindowHeight = height;

            if(disableMinimize)
            {
                DeleteMenu(GetSystemMenu(console, false), SC_MINIMIZE, MF_BYCOMMAND);
            }

            if(disableMaximize)
            {
                DeleteMenu(GetSystemMenu(console, false), SC_MAXIMIZE, MF_BYCOMMAND);
            }

            DeleteMenu(GetSystemMenu(console, false), SC_SIZE, MF_BYCOMMAND);
        }

        /// <summary>
        /// Flashes the <see cref="Console"/> window <paramref name="num"/> times. Stops when brought to the foreground if <paramref name="stopOnForeground"/> is true.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="stopOnForeground"></param>
        public static void FlashWindow(uint num = 5, bool stopOnForeground = true)
        {
            var fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = GetConsoleWindow();
            fInfo.dwFlags = stopOnForeground ? FLASHW_TIMERNOFG : FLASHW_TRAY;
            fInfo.uCount = num;
            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }

        /// <summary>
        /// Stops flashing the window
        /// </summary>
        public static void StopFlashWindow()
        {
            var fInfo = new FLASHWINFO();
            fInfo.hwnd = GetConsoleWindow();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.dwFlags = FLASHW_STOP;
        }

        /// <summary>
        /// Minimizes the console window. Returns <c>true</c> if the window was previously visible
        /// </summary>
        /// <returns>bool</returns>
        public static bool Minimize() => ShowWindow(GetConsoleWindow(), SW_MINIMIZE);
        /// <summary>
        /// Maximizes the console window. Returns <c>true</c> if the window was previously visible
        /// </summary>
        /// <returns>bool</returns>
        public static bool Maximize() => ShowWindow(GetConsoleWindow(), SW_MAXIMIZE);
        /// <summary>
        /// Restores the console window to its original size and position. Returns <c>true</c> if the window was previously visible
        /// </summary>
        /// <returns>bool</returns>
        public static bool Restore() => ShowWindow(GetConsoleWindow(), SW_SHOWNORMAL);
    }
}
