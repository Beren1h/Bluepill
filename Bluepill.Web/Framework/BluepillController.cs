using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Framework
{
    public abstract class BluepillController : Controller
    {
        //public ActionResult  GetView(dynamic model, string mobileView)
        //{
        //    //var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

        //    //if (Identity.IsMobile)
        //    //    return View(mobileView, model);

        //    return View(model);
        //}

        //public BluepillController()
        //{
        //    //ViewBag.IsMobile = "Fred";
        //}

        public BluePillIdentity Identity { get { return (BluePillIdentity)ControllerContext.HttpContext.User.Identity;  } }
    }
}