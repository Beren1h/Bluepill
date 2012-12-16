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
        private IFacetReader _reader;

        public InfoController(IFacetReader reader)
        {
            _reader = reader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            var facets = _reader.Read(identity.Name);

            var model = new InfoModel { MaxFacetValue = facets.Max(f => f.Id) };

            return View(model);
        }

    }
}
