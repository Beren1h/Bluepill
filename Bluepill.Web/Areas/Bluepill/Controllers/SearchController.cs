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
    public class SearchController : BluepillController
    {
        private IBoxPacker _packer;
        private IBoxStorage _attic;

        public SearchController(IBoxPacker packer, IBoxStorage attic)
        {
            _packer = packer;
            _attic = attic;
        }

        public ActionResult Test(int index)
        {
            var max = 14;
            ViewBag.upIndex = -1;
            ViewBag.downIndex = -1;

            ViewBag.start = string.Format("{0}.jpg", index);
            ViewBag.middle = string.Format("{0}.jpg", index + 1);
            ViewBag.end = string.Format("{0}.jpg", index + 2);


            if (index + 2 < max)
            {
                ViewBag.upIndex = index + 3;
            }

            if (index > 0)
            {
                ViewBag.downIndex = index - 3;
            }

            return View("Retreival");
        }

        public ActionResult Index()
        {
            //var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            //var facets = Identity.Facets;
            var model = new SearchModel { Facets = Identity.Facets, Page = 1, PageDelta = 0, IsMobile = Identity.IsMobile };

            //ViewBag.NavigationIndex = 1;

            return View(model);
        }
               

        public ActionResult Find(SearchModel model)
        {
            ControllerContext.HttpContext.Session.Clear();

            //var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            var results =_attic.GetBoxes(model.Facets, Constants.PER_PAGE, model.Page, Identity.Name);

            ControllerContext.HttpContext.Session.Add(Constants.RETRIEVAL_SESSION_KEY, results.Boxes);
            
            if (results.Boxes.Count == 0)
            {
                return PartialView("Empty");
            }

            model.MaxIndex = results.Boxes.Count - 1;
            model.TotalPages = Math.Ceiling((double)results.Total / Constants.PER_PAGE);
            model.TotalBoxes = results.Total;

            if(Identity.IsMobile)
                return View("RetreivalMobile", model);

            return View("Retreival", model);
        }
    }
}