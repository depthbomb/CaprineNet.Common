#region License
/*
    CaprineNet.Common.Extensions
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

using System.IO;

namespace CaprineNet.Common.Extensions
{
    public static class IOExtensions
    {
        public enum PathType
        {
            File,
            Directory
        }

        /// <summary>
        /// Creates a file or directory from <paramref name="path"/> depending on <paramref name="pathType"/>
        /// </summary>
        /// <param name="path">File path or directory path</param>
        /// <param name="pathType">Type to treat <paramref name="path"/> as</param>
        public static void CreateIfNotExists(this string path, PathType pathType)
        {
            switch(pathType)
            {
                case PathType.File:
                    if(!File.Exists(path))
                    {
                        File.Create(path).Dispose();
                    }
                    break;
                case PathType.Directory:
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns a file's size in bytes
        /// </summary>
        /// <param name="path"></param>
        public static long GetFileSize(this string path)
        {
            try
            {
                return new FileInfo(path).Length;
            }
            catch { }

            return -1;
        }

        /// <summary>
        /// Determines whether a directory is hidden
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static bool IsHidden(this DirectoryInfo directoryInfo) => (directoryInfo.Attributes & FileAttributes.Hidden) != 0;

        /// <summary>
        /// Determines whether a file is hidden
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static bool IsHidden(this FileInfo fileInfo) => (fileInfo.Attributes & FileAttributes.Hidden) != 0;
    }
}
