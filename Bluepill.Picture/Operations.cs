using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Picture
{
    public class Operations : IOperations
    {
        private readonly IResize _resize;

        public Operations()
        {
            _resize = new Resize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public byte[] Resize(string file, int width, int height)
        {
            var source = new Bitmap(file);
            var scale = _resize.DetermineResizeScale(source.Width, source.Height, width, height);

            return _resize.CreateResizedPicture(file, scale);

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        ///// <returns></returns>
        //public byte[] Resize(byte[] bytes, int width, int height)
        //{
        //    var source = Converter.GetBitmap(bytes);
        //    var scale = _resize.DetermineResizeScale(source.Width, source.Height, width, height);

        //    return _resize.CreateResizedPicture(source, scale);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns></returns>
        public bool Compare(byte[] image1, byte[] image2)
        {
            return image1.SequenceEqual(image2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Scale GetScale(byte[] bytes)
        {
            var scale = new Scale();

            using (var source = Converter.GetBitmap(bytes))
            {
                if (source != null)
                {
                    scale.Height = source.Height;
                    scale.Width = source.Width;
                }
            }

            return scale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] GetBytes(string file)
        {
            using (var source = new Bitmap(file))
            {
                using (var ms = new MemoryStream())
                {
                    source.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

    }
}
