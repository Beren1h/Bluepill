using Bluepill.Picture;
using Bluepill.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebConstants = Bluepill.Web.Framework.Constants;

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
                }

                var scale = _resize.DetermineResizeScale(source.Width, source.Height, width, height);
                var bytes = _resize.CreateResizedPicture(file, scale);

                return new FileContentResult(bytes, "image/png");
            }
        }

        public FileContentResult GetPicture(string file)
        {
            using (var source = new Bitmap(file))
            {
                using (var ms = new MemoryStream())
                {
                    source.Save(ms, ImageFormat.Png);
                    var bytes = ms.ToArray();
                    return new FileContentResult(bytes, "image/png");
                }
            }
        }

        public FileContentResult GetPictureBytes(int index)
        {
            var boxes = (IList<Box>)ControllerContext.HttpContext.Session[WebConstants.RETRIEVAL_SESSION_KEY];

            if (index < boxes.Count)
            {
                var box = boxes[index];
                return new FileContentResult(box.ReducedBytes, "image/png");
            }

            return null;

            
        }


    }
}
