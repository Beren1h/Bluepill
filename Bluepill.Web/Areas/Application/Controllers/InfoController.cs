using Bluepill.Search;
using Bluepill.Web.Areas.Application.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class InfoController : Controller
    {
        private IFacetCollectionReader _reader;

        public InfoController(IFacetCollectionReader reader)
        {
            _reader = reader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            var collections = _reader.GetFacetCollections(identity.Name, ControllerContext.HttpContext.Session);

            long max= -1;
            
            foreach (var collection in collections)
            {
               foreach (var facet in collection.Facets)
               {
                   foreach (var aspect in facet.Aspects)
                   {
                       max = (aspect.Value > max) ? aspect.Value : max;
                   }
               }
            }

            var model = new InfoModel { MaxAspectValue = max };

           return View(model);
        }

    }
}
