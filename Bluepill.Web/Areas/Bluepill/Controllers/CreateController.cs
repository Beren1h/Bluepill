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
//using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.Bluepill.Controllers
{
    public class CreateController : Controller
    {
        //private const string CREATE_PATH = "c:\\bluepill\\input";
        //private const string COMPLETE_PATH = "c:\\bluepill\\completed";
        //private const int DISPLAY_COUNT = 1;
        //private const int IMG_WIDTH = 600;
        //private const int IMG_HEIGHT = 600;

        private IFacetReader _reader;



        private IFacetCollectionReader _facetCollectionReader;
        private IPacker _packer;
        private IAttic _attic;
        private ICookieGateway _cookieGateway;

        public CreateController(IFacetReader reader, IFacetCollectionReader facetCollectionReader, IPacker packer, IAttic attic, ICookieGateway cookieGateway)
        {
            _facetCollectionReader = facetCollectionReader;
            _packer = packer;
            _attic = attic;
            _cookieGateway = cookieGateway;
            _reader = reader;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            //var collections = _facetCollectionReader.GetFacetCollections(identity.Name, Session);
            //var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            //var workingCollection = _cookieGateway.GetVale(ControllerContext.HttpContext, cookieName, Constants.WORKING_COLLECTION_COOKIE_KEY) ?? collections[0].Name;
            //var collection = collections.FirstOrDefault(c => c.Name == workingCollection);
            var files = new List<FileInfo>(new DirectoryInfo(Constants.CREATE_PATH).GetFiles());
            var list = files.Take(Constants.DISPLAY_COUNT).ToList();
            //var facets = _reader.BuildFacets(identity.Name);
            var facets = _reader.Read(identity.Name).Where(x => x.Top == true);
            //var longs = (from v in facets select v.Value).ToList();

            //var model = new CreateModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = Constants.IMG_HEIGHT, ResizedWidth = Constants.IMG_WIDTH };
            var model = new CreateModel { Facets = facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = Constants.IMG_HEIGHT, ResizedWidth = Constants.IMG_WIDTH };

            ViewBag.NavigationIndex = 0;

            return View(model);
        }

        public ActionResult GetFacet(string id)
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            //var facet = _reader.Read(identity.Name).Where(x => x.Id == id).FirstOrDefault();
            var facet = _reader.Read(identity.Name).Where(x => x.Id == id);

            //return View("EditorTemplates/Facet", facet);
            return View("Facet", facet);

        }

        //[HttpPost]
        //public JObject SavePicture(CreateModel model)
        //{
        //    var fileInfo = new FileInfo(model.File);
        //    var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
        //    var box = _packer.PackBox(model.File, identity.Name, model.Selects);
            
        //    _attic.AddBox(box);

        //    System.IO.File.Move(fileInfo.FullName, string.Format("{0}\\{1}", Constants.COMPLETE_PATH, fileInfo.Name));

        //    var files = new List<FileInfo>(new DirectoryInfo(Constants.CREATE_PATH).GetFiles());
        //    var list = files.Take(Constants.DISPLAY_COUNT).ToList();

        //    var json = new JObject();

        //    json.Add("file", list[0].FullName);
        //    json.Add("total", files.Count);
        //    json.Add("width", Constants.IMG_WIDTH);
        //    json.Add("height", Constants.IMG_HEIGHT);
        //    json.Add("src", string.Format(Constants.GET_PICTURE_URL_FORMAT, list[0].FullName));
        //    json.Add("resizedSrc", string.Format(Constants.GET_RESIZE_PICTURE_URL_FORMAT, list[0].FullName, Constants.IMG_WIDTH, Constants.IMG_HEIGHT));

        //    return json;
        //}
    }
}