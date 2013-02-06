using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Web.Areas.Administration.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.Administration.Controllers
{
    public class SearchController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;
        private IPacker _packer;
        private IAttic _attic;

        public SearchController(IFacetCollectionReader facetCollectionReader, IPacker packer, IAttic attic)
        {
            _facetCollectionReader = facetCollectionReader;
            _packer = packer;
            _attic = attic;
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
            //var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            //var list = files.Take(DISPLAY_COUNT).ToList();

            //var model = new CreateModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = IMG_HEIGHT, ResizedWidth = IMG_WIDTH };
            var model = new SearchModel { Facets = collection.Facets };

            ViewBag.NavigationIndex = 1;

            return View(model);
        }

        public ActionResult Find(SearchModel model)
        {
            //var box = _packer.PackBox("", "me", model.Facets);
            var x =_attic.GetBoxes(model.Facets,10,1);
            return View();
        }

    }
}
