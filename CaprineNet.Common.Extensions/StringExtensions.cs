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

using System;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace CaprineNet.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,125}[a-zA-Z]{2,63}$");

        private static readonly char[] InvalidFileNameCharacters = Path.GetInvalidFileNameChars();
        private static readonly char[] InvalidPathCharacters = Path.GetInvalidPathChars();

        /// <summary>
        /// Repeats the string a number of times
        /// </summary>
        public static string Repeat(this string str, int count) => new StringBuilder(str.Length * count).Insert(0, str, count).ToString();

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNumber(this string str) => int.TryParse(str, out int _);

        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static bool Contains(this string str, string value, StringComparison comparison) => str.IndexOf(value, comparison) >= 0;

        public static void Print(this string str, params object[] args) => Console.WriteLine(str, args);

        /// <summary>
        /// Compresses the string and encodes it to Base64
        /// </summary>
        public static string Compress(this string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);

            using (var ms = new MemoryStream())
            using (var zs = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zs.Write(buffer, 0, buffer.Length);

                ms.Position = 0;

                var compressed = new byte[ms.Length];
                ms.Read(compressed, 0, compressed.Length);

                var gZipBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gZipBuffer, 4, compressed.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);

                return Convert.ToBase64String(gZipBuffer);
            }
        }

        /// <summary>
        /// Decompresses a compressed, Base64-encoded string.
        /// <para>See <see cref="Compress(string)"/></para>
        /// </summary>
        public static string Decompress(this string str)
        {
            var gZipBuffer = Convert.FromBase64String(str);

            using (var ms = new MemoryStream())
            {
                var dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                ms.Write(gZipBuffer, 4, gZipBuffer.Length - 4);
                ms.Position = 0;

                var buffer = new byte[dataLength];
                using (var zs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zs.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// Converts an email string into a Gravatar URL
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToGravatar(this string str, int size = 200)
        {
            string md5Hash;
            var match = EmailRegex.Match(str);
            if (match.Success)
            {
                using(MD5 md5 = MD5.Create())
                {
                    byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                    var stringBuilder = new StringBuilder();

                    for(int i = 0; i < data.Length; i++)
                    {
                        stringBuilder.Append(data[i].ToString("x2"));
                    }

                    md5Hash = stringBuilder.ToString().ToLower().Replace("-", "");
                }

                return string.Format("https://www.gravatar.com/avatar/{0}?s={1}", md5Hash, size);
            }
            else
            {
                throw new ArgumentException("String is not a valid email address");
            }
        }

        public static bool IsValidFileName(this string str) => !str.IsNullOrEmpty() && str.IndexOfAny(InvalidFileNameCharacters) == -1;

        public static bool IsValidPathName(this string str) => !str.IsNullOrEmpty() && str.IndexOfAny(InvalidPathCharacters) == -1;
    }
}
