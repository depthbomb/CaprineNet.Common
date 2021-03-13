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

using System.Collections.Generic;

namespace CaprineNet.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Adds items from a <paramref name="collection"/> to the current <paramref name="list"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="collection"></param>
        public static void Add<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                list.Add(item);
            }
        }
    }
}
