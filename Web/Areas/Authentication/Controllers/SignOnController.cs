using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;
using Web.Areas.Authentication.Models;
using System.Web.Security;
using Web.Gateways;

namespace Web.Areas.Authentication.Controllers
{
    public class SignOnController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignOn(SignOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Create", new { area = "Administration" });
            }

            return View("Index", model);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}
