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

namespace CaprineNet.Common.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Determines if <paramref name="number"/> is even
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsEven(this int number) => number % 2 == 0;

        /// <summary>
        /// Determines if <paramref name="number"/> is odd
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsOdd(this int number) => !IsEven(number);

        public static bool IsEven(this uint number) => number % 2 == 0;

        public static bool IsOdd(this uint number) => !IsEven(number);

        public static bool IsEven(this long number) => number % 2 == 0;

        public static bool IsOdd(this long number) => !IsEven(number);

        public static bool IsEven(this ulong number) => number % 2 == 0;

        public static bool IsOdd(this ulong number) => !IsEven(number);
    }
}
