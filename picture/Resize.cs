using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Picture
{
    public class Resize : IResize
    {
        ///// <summary>
        /////  create resized image from file
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        ///// <returns></returns>
        //public byte[] Create(string file, int width, int height)
        //{
        //    var source = new Bitmap(file);
        //    var scale = DetermineResizeScale(source.Width, source.Height, width, height);

        //    return CreateResizedPicture(source, scale);
        //}

        ///// <summary>
        ///// create resize image from byte array
        ///// </summary>
        ///// <param name="array"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        ///// <returns></returns>
        //public byte[] Create(byte[] array, int width, int height)
        //{
        //    var converter = new ImageConverter();
        //    var source = (Bitmap)converter.ConvertFrom(array);
        //    var scale = DetermineResizeScale(source.Width, source.Height, width, height);

        //    return CreateResizedPicture(source, scale);
        //}

        /// <summary>
        /// 
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="scale"></param>
        ///// <returns></returns>
        //public byte[] CreateResizedPicture(Bitmap source, Scale scale)
        //{
        //    using (source)
        //    {
        //        using (var resized = new Bitmap(scale.Width, scale.Height))
        //        {
        //            using (var graphics = Graphics.FromImage(resized))
        //            {
        //                using (var ms = new MemoryStream())
        //                {
        //                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                    graphics.DrawImage(source, new Rectangle(0, 0, scale.Width, scale.Height));
        //                    resized.Save(ms, ImageFormat.Png);
        //                    return ms.ToArray();
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 
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
