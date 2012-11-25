﻿using Bluepill.Picture;
using Bluepill.Storage;
using Bluepill.Web.Framework;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class PictureController : Controller
    {
        private IResize _resize;
        private IAttic _attic;

        public PictureController(IResize resize, IAttic attic)
        {
            _resize = resize;
            _attic = attic;
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

        public FileContentResult GetPictureReducedBytes(int index)
        {
            var boxes = (IList<Box>)ControllerContext.HttpContext.Session[Constants.RETRIEVAL_SESSION_KEY];

            if (index < boxes.Count)
            {
                var box = boxes[index];
                return new FileContentResult(box.ReducedBytes, "image/png");
            }

            return null;
        }

        public FileContentResult GetPictureBytes(int index)
        {
            var boxes = (IList<Box>)ControllerContext.HttpContext.Session[Constants.RETRIEVAL_SESSION_KEY];
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            if (index < boxes.Count)
            {
                var box = boxes[index];
                var retrieval = _attic.GetBox(box._id, identity.Name, new[] { Fields.BYTES, Fields.IS_LARGE, Fields.GRIDFS_ID });

                return (retrieval.Boxes.Count > 0) ? new FileContentResult(retrieval.Boxes[0].Bytes, "image/png") : null;
            }

            return null;
        }

        public void RemovePicture(int index)
        {
            var boxes = (IList<Box>)ControllerContext.HttpContext.Session[Constants.RETRIEVAL_SESSION_KEY];
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            if (index < boxes.Count)
            {
                var box = boxes[index];
                var retrieval = _attic.GetBox(box._id, identity.Name, new[] { Fields.BYTES, Fields.IS_LARGE, Fields.GRIDFS_ID, Fields.OBJECT_ID });

                using (var ms = new MemoryStream(retrieval.Boxes[0].Bytes))
                {
                    using (var bitmap = new Bitmap(ms))
                    {
                        var removedFile = string.Format("{0}\\removed_{1}.png", "c:\\bluepill\\input", retrieval.Boxes[0]._id);
                        using (var fs = new FileStream(removedFile, FileMode.Create)) { }
                        bitmap.Save(removedFile);
                    }
                }

                _attic.RemoveBox(retrieval.Boxes[0]._id, identity.Name);

            }
        }




    }
}
