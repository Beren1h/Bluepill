using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Bluepill.Picture
{
    /// <summary>
    /// operations to change scale of picture
    /// </summary>
    public class Resize : IResize
    {
        /// <summary>
        /// determine the dimensions of a picture to fit a new size while maintaining aspect ratio
        /// </summary>
        /// <param name="sourceWidth"></param>
        /// <param name="sourceHeight"></param>
        /// <param name="resizedWidth"></param>
        /// <param name="resizedHeight"></param>
        /// <returns></returns>
        public Scale DetermineResizeScale(int sourceWidth, int sourceHeight, int resizedWidth, int resizedHeight)
        {
            if (sourceHeight == resizedHeight && sourceWidth == resizedWidth)
                return new Scale { Width = sourceWidth, Height = sourceHeight };

            float ratioWidth = (float)resizedWidth / (float)sourceWidth;
            float ratioHeight = (float)resizedHeight / (float)sourceHeight;

            if (ratioHeight < ratioWidth)
                return new Scale { Height = resizedHeight, Width = (int)(sourceWidth * ratioHeight) };

            return new Scale { Height = (int)(sourceHeight * ratioWidth), Width = resizedWidth };
        }

        /// <summary>
        /// create a new bitmap with new dimnesions from an existing picture
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public byte[] CreateResizedPicture(string file, Scale scale)
        {
            using (var source = new Bitmap(file))
            {
                using (var resized = new Bitmap(scale.Width, scale.Height))
                {
                    using (var graphics = Graphics.FromImage(resized))
                    {
                        using (var ms = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(source, new Rectangle(0, 0, scale.Width, scale.Height));
                            resized.Save(ms, ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }



    }
}
