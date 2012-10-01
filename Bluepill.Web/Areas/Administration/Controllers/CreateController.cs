using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bluepill.Web.Framework;

namespace Bluepill.Web.Areas.Administration.Controllers
{
    public class CreateController : Controller
    {
        private IFacet _facet;

        public CreateController(IFacet facet)
        {
            _facet = facet;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var facets = _facet.GetUserFacets(identity.Name);
            return View();
        }

    }
}
