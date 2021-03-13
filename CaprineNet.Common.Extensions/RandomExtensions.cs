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
using System.Linq;
using System.Collections.Generic;

namespace CaprineNet.Common.Extensions
{
    public static class RandomExtensions
    {
        public static T RandomListItem<T>(this Random rng, List<T> list)
            => list[rng.Next(list.Count)];

        public static int[] Sequence(this Random rng, int count, int min, int max)
        {
            var candidates = new HashSet<int>();

            for(var top = max - count; top < max; top++)
            {
                if(!candidates.Add(rng.Next(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }

            var result = candidates.ToArray();
            for(var i = result.Length - 1; i > 0; i--)
            {
                var k = rng.Next(i + 1);
                var tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }

            return result;
        }
    }
}
