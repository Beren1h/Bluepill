using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Application.Models;
using Web.Framework;

namespace Web.Areas.Application.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var list = new SelectList(identity.Collections, FindWorkingCollection(identity.Name));

            var model = new SettingsModel { CollectionsDropDown = list, UserName = identity.Name };

            return View(model);
        }

        public ActionResult Update(SettingsModel model)
        {
            var cookieName = string.Format("bluepill.{0}.preferences", model.UserName);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie == null)
            {
                userCookie = new HttpCookie(cookieName);
                userCookie.Expires = DateTime.Now.AddYears(14);
            }

            userCookie.Values["wc"] = model.Collections.FirstOrDefault().ToString();

            ControllerContext.HttpContext.Response.Cookies.Add(userCookie);

            return RedirectToAction("index", "settings", new { Area = "application" });
        }

        private string FindWorkingCollection(string userName)
        {
            var cookieName = string.Format("bluepill.{0}.preferences", userName);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie != null)
            {
                return userCookie.Values["wc"];
            }

            return "";
        }
    }
}
