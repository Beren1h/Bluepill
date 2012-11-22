using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Web.Areas.Application.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class SearchInterfaceController : Controller
    {
        private const string CREATE_PATH = "c:\\bluepill\\input";
        private const string COMPLETE_PATH = "c:\\bluepill\\completed";
        private const int DISPLAY_COUNT = 1;
        private const int IMG_WIDTH = 600;
        private const int IMG_HEIGHT = 600;
        
        private IFacetCollectionReader _facetCollectionReader;

        public SearchInterfaceController(IFacetCollectionReader facetCollectionReader)
        {
            _facetCollectionReader = facetCollectionReader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var collections = _facetCollectionReader.GetFacetCollections(identity.Name, Session);

            var cookies = ControllerContext.HttpContext.Request.Cookies;
            var cookieName = string.Format(Bluepill.Web.Framework.Constants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var userCookie = cookies[cookieName];

            var workingCollection = (userCookie != null) ? userCookie.Values[Bluepill.Web.Framework.Constants.WORKING_COLLECTION_COOKIE_KEY] : collections[0].Name;
            var collection = collections.FirstOrDefault(c => c.Name == workingCollection);
            var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            var list = files.Take(DISPLAY_COUNT).ToList();

            var model = new SearchInterfaceModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = IMG_HEIGHT, ResizedWidth = IMG_WIDTH };

            ViewBag.NavigationIndex = 0;

            return View(model);
        }

    }
}
