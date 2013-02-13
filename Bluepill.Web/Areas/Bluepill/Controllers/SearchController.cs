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

namespace Bluepill.Web.Areas.Bluepill.Controllers
{
    public class SearchController : Controller
    {
        private IBoxPacker _packer;
        private IBoxStorage _attic;
        private IFacetReader _facetReader;

        public SearchController(IFacetReader facetReader, IBoxPacker packer, IBoxStorage attic)
        {
            _packer = packer;
            _attic = attic;
            _facetReader = facetReader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var facets = _facetReader.Read(identity.Name);
            var model = new SearchModel { Facets = facets, Page = 1, PageDelta = 0 };

            ViewBag.NavigationIndex = 1;

            return View(model);
        }
               

        public ActionResult Find(SearchModel model)
        {
            ControllerContext.HttpContext.Session.Clear();

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            var results =_attic.GetBoxes(model.Facets, Constants.PER_PAGE, model.Page, identity.Name);

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