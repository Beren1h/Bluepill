using Bluepill.Picture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class PictureController : Controller
    {
        private IResize _resize;

        public PictureController(IResize resize)
        {
            _resize = resize;
        }

        public FileContentResult GetResizePicture(string file, int width, int height)
        {
            
            using (var source = new Bitmap(file))
            {
                using (var ms = new MemoryStream())
                {
                    source.Save(ms, ImageFormat.Png);
                    //return ms.ToArray();
                }

                var scale = _resize.DetermineResizeScale(source.Width, source.Height, width, height);
                var bytes = _resize.CreateResizedPicture(file, scale);

                return new FileContentResult(bytes, "image/png");
            }

            
        }

    }
}
