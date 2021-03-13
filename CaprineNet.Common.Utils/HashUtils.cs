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
using System.Security.Cryptography;

namespace CaprineNet.Common.Utils
{
    /// <summary>
    /// Contains helper methods for dealing with file hashing
    /// </summary>
    public static class HashUtils
    {
        public enum Algorithms
        {
            Md5,
            Sha256,
            Sha384,
            Sha512
        }

        /// <summary>
        /// Hashes a <paramref name="file"/> based on the <paramref name="algorithm"/>
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <param name="algorithm">Hashing algorithm</param>
        public static string HashFile(string file, Algorithms algorithm = Algorithms.Md5)
        {
            Ensure.Exists(new FileInfo(file));

            using(FileStream stream = File.OpenRead(file))
            {
                byte[] checksum;
                switch(algorithm)
                {
                    default:
                    case Algorithms.Md5:
                        checksum = Md5Hash(stream);
                        break;
                    case Algorithms.Sha256:
                        checksum = Sha256Hash(stream);
                        break;
                    case Algorithms.Sha384:
                        checksum = Sha384Hash(stream);
                        break;
                    case Algorithms.Sha512:
                        checksum = Sha512Hash(stream);
                        break;
                }

                return checksum.EncodeBytesToString();
            }
        }

        /// <summary>
        /// Determines whether a <paramref name="file"/> matches the provided <paramref name="hash"/> based on the <paramref name="algorithm"/>
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <param name="hash"></param>
        /// <param name="algorithm">Hashing algorithm</param>
        public static bool HashMatches(string file, string hash, Algorithms algorithm = Algorithms.Md5)
        {
            Ensure.NotNullOrEmptyOrWhitespace(hash);

            return HashFile(file, algorithm) == hash;
        }

        #region Private methods
        private static byte[] Md5Hash(FileStream stream)
        {
            using(MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(stream);
            }
        }

        private static byte[] Sha256Hash(FileStream stream)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(stream);
            }
        }

        private static byte[] Sha384Hash(FileStream stream)
        {
            using(SHA384 sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(stream);
            }
        }

        private static byte[] Sha512Hash(FileStream stream)
        {
            using(SHA512 sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(stream);
            }
        }

        /// <summary>
        /// Encodes a <c>byte[]</c> checksum into a standard hash string format
        /// </summary>
        /// <param name="checksum"></param>
        /// <returns></returns>
        private static string EncodeBytesToString(this byte[] checksum)
            => BitConverter.ToString(checksum).Replace("-", string.Empty);
        #endregion
    }
}
