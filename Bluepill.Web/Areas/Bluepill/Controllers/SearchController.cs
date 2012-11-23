using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Web.Areas.Administration.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.Bluepill.Controllers
{
    public class SearchController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;
        private IPacker _packer;
        private IAttic _attic;
        private const int PER_PAGE = 10;
        private const string RETRIEVAL_KEY = "";

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
            var cookieName = string.Format(WebConstants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var userCookie = cookies[cookieName];
            
            var workingCollection = (userCookie != null) ? userCookie.Values[WebConstants.WORKING_COLLECTION_COOKIE_KEY] : collections[0].Name;
            var collection = collections.FirstOrDefault(c => c.Name == workingCollection);
            //var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            //var list = files.Take(DISPLAY_COUNT).ToList();

            //var model = new CreateModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = IMG_HEIGHT, ResizedWidth = IMG_WIDTH };
            var model = new SearchModel { Facets = collection.Facets, Page = 1, PageDelta = 0 };

            ViewBag.NavigationIndex = 1;

            return View(model);
        }
               

        public ActionResult Find(SearchModel model)
        {
            //if (model.PageDelta > 0 && model.Page < model.TotalPages)
            //{
            //    model.Page++;
            //}

            //if (model.PageDelta < 0 && model.Page > 1)
            //{
            //    model.Page--;
            //}
            
            ControllerContext.HttpContext.Session.Clear();

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var results =_attic.GetBoxes(model.Facets, PER_PAGE, model.Page,identity.Name);

            ControllerContext.HttpContext.Session.Add(WebConstants.RETRIEVAL_SESSION_KEY, results.Boxes);
            
            if (results.Boxes.Count == 0)
            {
                return PartialView("Empty");
            }

            model.MaxIndex = results.Boxes.Count - 1;
            model.TotalPages = Math.Ceiling((double)results.Total / PER_PAGE);
            model.TotalBoxes = results.Total;

            return View("Retreival", model);
            
        }

    }
}
