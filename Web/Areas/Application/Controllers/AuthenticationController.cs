﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Areas.Application.Models;

namespace Web.Areas.Application.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignOn(AuthenticationModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("index", "create", new { area = "administration" });
            }

            return View("index", model);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}
