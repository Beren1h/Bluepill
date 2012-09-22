using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;

namespace Web.Areas.Administration.Controllers
{
    public class CreateController : Controller
    {
        //
        // GET: /Administration/Create/

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            return View();
        }

    }
}
