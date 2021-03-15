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
using System.Drawing;

namespace CaprineNet.Common.Utils
{
    public static class ImageUtils
    {
        /// <summary>
        /// Gets the width of an image from its <paramref name="path"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>An integer representing the image's width</returns>
        public static int GetWidth(string path)
        {
            var values = GetResolution(path);

            return values.Item1;
        }

        /// <summary>
        /// Gets the width of an image from its <paramref name="path"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>An integer representing the image's height</returns>
        public static int GetHeight(string path)
        {
            var values = GetResolution(path);

            return values.Item2;
        }

        /// <summary>
        /// Gets the width and height of an image from its <paramref name="path"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A tuple containing the width and height in that order</returns>
        public static Tuple<int, int> GetResolution(string path)
        {
            Ensure.Exists(new FileInfo(path));

            using (var image = new Bitmap(path))
            {
                return Tuple.Create(image.Width, image.Height);
            }
        }
    }
}
