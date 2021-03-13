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
using System.Security.Principal;

namespace CaprineNet.Common.Utils
{
    public static class Utils
    {
        private static readonly Version OsVersion = Environment.OSVersion.Version;

        #region OS Detectors
        public static bool IsWindowsXP() => OsVersion.Major == 5 && OsVersion.Minor == 1;
        public static bool IsWindowsXPOrGreater() => (OsVersion.Major == 5 && OsVersion.Minor >= 1) || OsVersion.Major > 5;
        public static bool IsWindowsVista() => OsVersion.Major == 6;
        public static bool IsWindowsVistaOrGreater() => OsVersion.Major >= 6;
        public static bool IsWindows7() => OsVersion.Major == 6 && OsVersion.Minor == 1;
        public static bool IsWindows7OrGreater() => (OsVersion.Major == 6 && OsVersion.Minor >= 1) || OsVersion.Major > 6;
        public static bool IsWindows8() => OsVersion.Major == 6 && OsVersion.Minor == 2;
        public static bool IsWindows8OrGreater() => (OsVersion.Major == 6 && OsVersion.Minor >= 2) || OsVersion.Major > 6;
        public static bool IsWindows10OrGreater(int build = -1) => OsVersion.Major >= 10 && OsVersion.Build >= build;
        #endregion

        /// <summary>
        /// Determines if the assembly has administrator priviledges
        /// </summary>
        /// <returns><c>true</c> if the assembly is has administrator priviledges</returns>
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
