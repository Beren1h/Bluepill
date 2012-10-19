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
using Newtonsoft.Json.Linq;

namespace Bluepill.Web.Areas.Administration.Controllers
{
    public class CreateController : Controller
    {
        private const string CREATE_PATH = "c:\\bluepill\\input";
        private const string COMPLETE_PATH = "c:\\bluepill\\completed";
        private const int DISPLAY_COUNT = 1;
        private const int IMG_WIDTH = 600;
        private const int IMG_HEIGHT = 600;
        
        private IFacetCollectionReader _facetCollectionReader;
        private IPacker _packer;
        private IAttic _attic;

        public CreateController(IFacetCollectionReader facetCollectionReader, IPacker packer, IAttic attic)
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
            var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            var list = files.Take(DISPLAY_COUNT).ToList();

            var model = new CreateModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = IMG_HEIGHT, ResizedWidth = IMG_WIDTH };

            ViewBag.NavigationIndex = 0;

            return View(model);
        }

        [HttpPost]
        public JObject SavePicture(CreateModel model)
        {
            var fileInfo = new FileInfo(model.File);
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var box = _packer.PackBox(model.File, identity.Name, model.Facets);
            
            _attic.AddBox(box);

            System.IO.File.Move(fileInfo.FullName, string.Format("{0}\\{1}", COMPLETE_PATH, fileInfo.Name));

            var files = new List<FileInfo>(new DirectoryInfo(CREATE_PATH).GetFiles());
            var list = files.Take(DISPLAY_COUNT).ToList();

            var json = new JObject();

            json.Add("file", list[0].FullName);
            json.Add("total", files.Count);
            json.Add("width", IMG_WIDTH);
            json.Add("height", IMG_HEIGHT);
            json.Add("src", string.Format("\\application\\picture\\getpicture?file={0}",list[0].FullName));
            json.Add("resizedSrc", string.Format("\\application\\picture\\getresizepicture?file={0}&width=600&height=600",list[0].FullName));

            return json;
        }
    }
}