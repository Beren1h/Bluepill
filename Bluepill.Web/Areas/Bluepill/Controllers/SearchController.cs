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
//using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.Bluepill.Controllers
{
    public class SearchController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;
        private IPacker _packer;
        private IAttic _attic;
        private ICookieGateway _cookieGateway;

        public SearchController(IFacetCollectionReader facetCollectionReader, IPacker packer, IAttic attic, ICookieGateway cookieGateway)
        {
            _facetCollectionReader = facetCollectionReader;
            _packer = packer;
            _attic = attic;
            _cookieGateway = cookieGateway;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var collections = _facetCollectionReader.GetFacetCollections(identity.Name, Session);
            var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var workingCollection = _cookieGateway.GetVale(ControllerContext.HttpContext, cookieName, Constants.WORKING_COLLECTION_COOKIE_KEY) ?? collections[0].Name;
            var collection = collections.FirstOrDefault(c => c.Name == workingCollection);
            var model = new SearchModel { Facets = collection.Facets, Page = 1, PageDelta = 0 };

            ViewBag.NavigationIndex = 1;

            return View(model);
        }
               

        public ActionResult Find(SearchModel model)
        {
            ControllerContext.HttpContext.Session.Clear();

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var results =_attic.GetBoxes(model.Facets, Constants.PER_PAGE, model.Page,identity.Name);

            ControllerContext.HttpContext.Session.Add(Constants.RETRIEVAL_SESSION_KEY, results.Boxes);
            
            if (results.Boxes.Count == 0)
            {
                return PartialView("Empty");
            }

            model.MaxIndex = results.Boxes.Count - 1;
            model.TotalPages = Math.Ceiling((double)results.Total / Constants.PER_PAGE);
            model.TotalBoxes = results.Total;

            return View("Retreival", model);
        }

    }
}
