﻿using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Web.Areas.Application.Models;
using Bluepill.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebConstants = Bluepill.Web.Framework.Constants;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class SearchInterfaceController : Controller
    {
        private IFacetCollectionReader _facetCollectionReader;
        private ICookieGateway _cookieGateway;

        public SearchInterfaceController(IFacetCollectionReader facetCollectionReader, ICookieGateway cookieGateway)
        {
            _facetCollectionReader = facetCollectionReader;
            _cookieGateway = cookieGateway;
        }

        public ActionResult Index()
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var collections = _facetCollectionReader.GetFacetCollections(identity.Name, Session);
            var cookieName = string.Format(Constants.PREFERENCE_COOKIE_FORMAT, identity.Name);
            var workingCollection = _cookieGateway.GetVale(ControllerContext.HttpContext, cookieName, Constants.WORKING_COLLECTION_COOKIE_KEY) ?? collections[0].Name;
            var collection = collections.FirstOrDefault(c => c.Name == workingCollection);
            var files = new List<FileInfo>(new DirectoryInfo(Constants.CREATE_PATH).GetFiles());
            var list = files.Take(Constants.DISPLAY_COUNT).ToList();

            var model = new SearchInterfaceModel { Facets = collection.Facets, File = list[0].FullName, TotalFileCount = files.Count, ResizedHeight = Constants.IMG_HEIGHT, ResizedWidth = Constants.IMG_WIDTH };

            ViewBag.NavigationIndex = 0;

            return View(model);
        }

    }
}