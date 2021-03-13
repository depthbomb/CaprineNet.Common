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

using System.IO;
using System.Diagnostics;

namespace CaprineNet.Common.Utils
{
    public static class IOUtils
    {
        /// <summary>
        /// Moves a file to an existing folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="overwrite"></param>
        /// <returns>Path to new file</returns>
        public static string MoveFile(string filePath, string destinationFolder, bool overwrite = true)
        {
            Ensure.Exists(new FileInfo(filePath));
            Ensure.Exists(new DirectoryInfo(destinationFolder));

            string fileName = Path.GetFileName(filePath);
            string destinationFilePath = Path.Combine(destinationFolder, fileName);

            if(overwrite && File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }

            File.Move(filePath, destinationFilePath);

            return destinationFilePath;
        }

        /// <summary>
        /// Copies a file to an existing folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="overwrite"></param>
        /// <returns>Path to the copied file</returns>
        public static string CopyFile(string filePath, string destinationFolder, bool overwrite = true)
        {
            Ensure.Exists(new FileInfo(filePath));
            Ensure.Exists(new DirectoryInfo(destinationFolder));

            if(!string.IsNullOrEmpty(filePath) && File.Exists(filePath) && !string.IsNullOrEmpty(destinationFolder))
            {
                string fileName = Path.GetFileName(filePath);
                string destinationFilePath = Path.Combine(destinationFolder, fileName);

                File.Copy(filePath, destinationFilePath, overwrite);

                return destinationFilePath;
            }

            return null;
        }

        /// <summary>
        /// Renames a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFileName"></param>
        /// <returns>Path to the newly-renamed file</returns>
        public static string RenameFile(string filePath, string newFileName)
        {
            Ensure.Exists(new FileInfo(filePath));
            Ensure.NotNullOrEmptyOrWhitespace(newFileName);

            string directory = Path.GetDirectoryName(filePath);
            string newFilePath = Path.Combine(directory, newFileName);
            File.Move(filePath, newFilePath);

            return newFilePath;
        }

        /// <summary>
        /// Determines whether a file is locked
        /// </summary>
        /// <param name="path"></param>
        /// <returns><c>true</c> if the file is locked</returns>
        public static bool IsFileLocked(string path)
        {
            Ensure.Exists(new FileInfo(path));

            try
            {
                using(var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)) { }
            }
            catch(IOException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Opens a file in the user's default program
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenFile(string filePath) => Process.Start("explorer.exe", filePath);

        /// <summary>
        /// Opens the folder containing <paramref name="filePath"/> in Explorer and selects the file
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenFolderAndSelect(string filePath)
        {
            Ensure.Exists(new FileInfo(filePath));

            string args = string.Format("/e, /select, \"{0}\"", filePath);

            var pInfo = new ProcessStartInfo()
            {
                FileName = "explorer",
                Arguments = args
            };

            Process.Start(pInfo);
        }
    }
}
