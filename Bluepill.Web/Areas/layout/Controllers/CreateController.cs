using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Web.Areas.layout.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.layout.Controllers
{
    public class CreateController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;

        public CreateController(IFacetCollectionReader facetCollectionReader)
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
            var cookieName = string.Format(WebConstants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var userCookie = cookies[cookieName];

            string workingCollection = "";

            if (userCookie != null)
            {
                workingCollection = userCookie.Values[WebConstants.WORKING_COLLECTION_COOKIE_KEY];

                if (!collectionNames.Contains(workingCollection))
                    workingCollection = collectionNames[0];

            }

            var model = new CreateModel { CollectionsDropDown = list, UserName = identity.Name, Collections = identity.Collections, WorkingCollection = workingCollection };

            return View(model);
        }

        private string ReadWorkingCollection(string userName)
        {
            var cookieName = string.Format(WebConstants.PREFERENCE_COOKIE_FORMAT, userName);
            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var userCookie = cookies[cookieName];

            if (userCookie != null)
            {
                return userCookie.Values[WebConstants.WORKING_COLLECTION_COOKIE_KEY];
            }

            return "";
        }
    }
}
