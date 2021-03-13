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
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CaprineNet.Common
{
    public static class Ensure
    {
        #region Ensure.That
        /// <summary>
        /// Ensures that <paramref name="condition"/> is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void That<TException>(bool condition, string message = "The condition is false.") where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        /// <summary>
        /// Ensures that <paramref name="condition"/> is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void That(bool condition, string message = "The condition is false.")
            => That<ArgumentException>(condition, message);
        #endregion

        #region Ensure.Not
        /// <summary>
        /// Ensures that the <paramref name="condition"/> is <i>not</i> true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Not<TException>(bool condition, string message = "The condition is true.") where TException : Exception
            => That<TException>(!condition, message);

        /// <summary>
        /// Ensures that the <paramref name="condition"/> is <i>not</i> true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Not(bool condition, string message = "The condition is true.")
            => Not<ArgumentException>(condition, message);
        #endregion

        #region Ensure.NotNull
        /// <summary>
        /// Ensures that <paramref name="value"/> is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public static T NotNull<T>(T value, string argName) where T : class
        {
            if (string.IsNullOrEmpty(argName) || string.IsNullOrWhiteSpace(argName))
            {
                argName = "Invalid";
            }

            That<ArgumentException>(value != null, argName);

            return value;
        }
        #endregion

        /// <summary>
        /// Ensures that the <paramref name="left"/> argument is equal to <paramref name="right"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="message"></param>
        public static void Equal<T>(T left, T right, string message = "Values must be equal.")
            => That<ArgumentException>(Comparer<T>.Default.Compare(left, right) == 0, message);

        /// <summary>
        /// Ensures that the <paramref name="left"/> argument is <i>not</i> equal to <paramref name="right"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="message"></param>
        public static void NotEqual<T>(T left, T right, string message = "Values must not be equal")
            => That<ArgumentException>(Comparer<T>.Default.Compare(left, right) != 0, message);

        /// <summary>
        /// Ensures that the <paramref name="collection"/> is not null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> collection, string message = "Collection must not be null or empty.")
        {
            NotNull(collection, nameof(collection));
            Not<ArgumentException>(!collection.Any(), message);

            return collection;
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not null, empty, or a whitespace character
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string NotNullOrEmptyOrWhitespace(string value, string message = "String must not be null, empty, or whitspace.")
        {
            That<ArgumentException>((string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)), message);

            return value;
        }

        /// <summary>
        /// Ensures that a directory exists
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static DirectoryInfo Exists(DirectoryInfo directoryInfo)
        {
            NotNull(directoryInfo, nameof(directoryInfo));

            directoryInfo.Refresh();

            That<DirectoryNotFoundException>(directoryInfo.Exists, $"{directoryInfo.FullName} could not be located.");

            return directoryInfo;
        }

        /// <summary>
        /// Ensures that a file exists
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static FileInfo Exists(FileInfo fileInfo)
        {
            NotNull(fileInfo, nameof(fileInfo));

            fileInfo.Refresh();

            That<FileNotFoundException>(fileInfo.Exists, $"{fileInfo.FullName} could not be located");

            return fileInfo;
        }
    }
}
