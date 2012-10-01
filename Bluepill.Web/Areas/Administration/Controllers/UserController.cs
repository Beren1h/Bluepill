using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Administration.Models;
using Web.Framework;
using Web.Gateways;

namespace Web.Areas.Administration.Controllers
{
    public class UserController : Controller
    {
        private ICookieGateway _cookieGateway;

        public UserController(ICookieGateway cookieGateway)
        {
            _cookieGateway = cookieGateway;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var list = new SelectList(identity.Collections,"collection1");
            
            var model = new UserModel { CollectionsDropDown = list, UserName = identity.Name };

            return View(model);
        }

        [HttpPost]
        public void Update(UserModel model)
        {
            var cookieName = string.Format("bluepill.{0}.preferences", model.UserName);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie == null)
            {
                userCookie = new HttpCookie(cookieName);
                userCookie.Expires = DateTime.Now.AddYears(14);
            }

            userCookie.Values["default_collection"] = model.Collections.FirstOrDefault().ToString();

            ControllerContext.HttpContext.Response.Cookies.Add(userCookie);

            //return View("Index");
        }

        public string Collection()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var cookieName = string.Format("bluepill.{0}.preferences", identity.Name);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie == null)
            {
                userCookie = new HttpCookie(cookieName);
                userCookie.Expires = DateTime.Now.AddYears(14);
            }
            //userCookie.Values["default_collection"] = model.Collections.FirstOrDefault().ToString();
            ControllerContext.HttpContext.Response.Cookies.Add(userCookie);
            //return 
            return identity.Name;

        }

    }


}
