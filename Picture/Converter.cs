using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture
{
    internal static class Converter
    {
        public static Bitmap GetBitmap(byte[] bytes)
        {
            var converter = new ImageConverter();
            return (Bitmap)converter.ConvertFrom(bytes);
        }
    }
}
