using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bluepill.Web.Framework;
using Bluepill.Storage;
using Bluepill.Picture;

namespace Bluepill.Web.Areas.Administration.Controllers
{
    public class CreateController : Controller
    {
        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            return View();
        }
    }
}
