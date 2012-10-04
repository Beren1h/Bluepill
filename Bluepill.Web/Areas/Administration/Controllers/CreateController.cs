using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bluepill.Web.Framework;
using Bluepill.Storage;
using Bluepill.Picture;
using System.IO;
using Bluepill.Web.Areas.Administration.Models;

namespace Bluepill.Web.Areas.Administration.Controllers
{
    public class CreateController : Controller
    {
        private const string CREATE_PATH = "c:\\bluepill";
        private const int DISPLAY_COUNT = 21;
        
        private IFacetCollectionReader facetCollectionReader;

        public CreateController(IFacetCollectionReader _facetCollectionReader)
        {
            _facetCollectionReader = facetCollectionReader;
        }

        public ActionResult Index()
        {
            var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            var list = files.Take(DISPLAY_COUNT).ToList();
            var model = new CreateModel { Facets = null, Files = list, TotalFileCount = files.Count };
            //var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            return View(model);
        }
    }
}
