using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bluepill.Web.Areas.Application.Models;
using Bluepill.Web.Framework;
using Bluepill.Search;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class SettingsController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;

        public SettingsController(IFacetCollectionReader facetCollectionReader)
        {
            _facetCollectionReader = facetCollectionReader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var collections = _facetCollectionReader.GetFacetCollections(identity.Name, Session);
            var collectionNames = collections.Select(c => c.Name).ToList();
            var list = new SelectList(collectionNames, ReadWorkingCollection(identity.Name));
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var userCookie = cookies[cookieName];

            string workingCollection = "";

            if (userCookie != null)
            {
                workingCollection = userCookie.Values[Constants.WORKING_COLLECTION_COOKIE_KEY];

                if (!collectionNames.Contains(workingCollection))
                    workingCollection = collectionNames[0];

            }

            var model = new SettingsModel { CollectionsDropDown = list, UserName = identity.Name, Collections = identity.Collections, WorkingCollection = workingCollection };

            return View(model);
        }

        public void Update(string uid, string selected)
        {
            var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, uid);
            var userCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddYears(14) };

            userCookie.Values[Constants.WORKING_COLLECTION_COOKIE_KEY] = selected;

            ControllerContext.HttpContext.Response.Cookies.Add(userCookie);
        }

        private string ReadWorkingCollection(string userName)
        {
            var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, userName);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie != null)
            {
                return userCookie.Values[Constants.WORKING_COLLECTION_COOKIE_KEY];
            }

            return "";
        }
    }
}
