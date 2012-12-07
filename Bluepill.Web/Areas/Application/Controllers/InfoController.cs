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
        //private IFacetCollectionReader _reader;
        private IFacetReader _reader;

        public InfoController(IFacetReader reader)
        {
            _reader = reader;
        }

        private long FindMax(IEnumerable<Facet> facets, long max)
        {
            //foreach(var facet in facets)
            //{
            //    if (max < facet.Value)
            //        max = facet.Value;

            //    max = FindMax(facet.Children, max);

            //}


            return max;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            //var collections = _reader.GetFacetCollections(identity.Name, ControllerContext.HttpContext.Session);
            var facets = _reader.BuildFacets(identity.Name);

            long max = -1;

            max = FindMax(facets, max);
            
            //foreach (var collection in collections)
            //{
            //   foreach (var facet in collection.Facets)
            //   {
            //       foreach (var aspect in facet.Aspects)
            //       {
            //           max = (aspect.Value > max) ? aspect.Value : max;
            //       }
            //   }
            //}

            var model = new InfoModel { MaxFacetValue = max };

           return View(model);
        }

    }
}
