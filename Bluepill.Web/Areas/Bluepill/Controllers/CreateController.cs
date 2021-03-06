﻿using Bluepill.Search;
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
using Bluepill.Dropbox;
using System.Threading.Tasks;
using System.Net.Http;

namespace Bluepill.Web.Areas.Bluepill.Controllers
{
    public class CreateController : BluepillController
    {
        private IBoxPacker _packer;
        private IBoxStorage _attic;
        private IApiRequest _dropbox;
        private List<string> _mimeTypes;
        private HttpClient _client;

        public CreateController(IBoxPacker packer, IBoxStorage attic, IApiRequest dropbox)
        {
            _packer = packer;
            _attic = attic;
            _dropbox = dropbox;
            _mimeTypes = new List<string> { "image/jpeg", "image/png" };
            _client = new HttpClient();
            //ViewBag.IsMobile = Identity.IsMobile;
        }


        public async Task<ActionResult> Index()
        {
            var metadata = await _dropbox.GetMetaData(Identity.AccessToken);
            var facets = Identity.Facets;
            var list = GetFileListFromMetaData(metadata);

            var model = new CreateModel
            {
                Facets = facets,
                TotalFileCount = 0,
                Url = "",
                ResizedHeight = Constants.IMG_HEIGHT,
                ResizedWidth = Constants.IMG_WIDTH,
                File = ""
            };

            if (list.Count > 0)
            {
                var media = await _dropbox.GetMedia(Identity.AccessToken, list[0]);
                var url = GetUrlFromMedia(media);
                
                model.TotalFileCount = list.Count();
                model.Url = url;
                model.File = list[0];
            }

            ViewBag.IsMobile = Identity.IsMobile;

            return View(model);
        }

        [HttpPost]
        public async Task<JObject> SavePicture(CreateModel model)
        {
            var bytes = await _client.GetByteArrayAsync(model.Url);

            var box = _packer.PackBox(bytes, Identity.Name, model.Facets);

            _attic.AddBox(box);

            await _dropbox.Delete(Identity.AccessToken, model.File);

            var metadata = await _dropbox.GetMetaData(Identity.AccessToken);

            var list = GetFileListFromMetaData(metadata);

            var json = new JObject();

            json.Add("total", list.Count);

            if (list.Count > 0)
            {
                var media = await _dropbox.GetMedia(Identity.AccessToken, list[0]);
                var url = GetUrlFromMedia(media);

                json.Add("width", Constants.IMG_WIDTH);
                json.Add("height", Constants.IMG_HEIGHT);
                json.Add("file", list[0]);
                json.Add("url", url);
                json.Add("src", string.Format(Constants.GET_PICTURE_URL_FORMAT, url));
                json.Add("resizedSrc", string.Format(Constants.GET_RESIZE_PICTURE_URL_FORMAT, url, Constants.IMG_WIDTH, Constants.IMG_HEIGHT));
            }
            
            return json;
        }

        private async Task<byte[]> GetPictureBytes(string url)
        {
            var client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url);
            return bytes;
        }

        private async void Delete(string file)
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            await _dropbox.Delete(identity.AccessToken, file);
        }

        private List<string> GetFileListFromMetaData(JObject content)
        {
            var json = from item in content["contents"] select JObject.Parse(item.ToString());

            var list = (from item in json.Where(x => x.Property("mime_type") != null &&
                            _mimeTypes.Contains(x.Property("mime_type").Value.ToString()))
                            select item.Property("path").Value.ToString()).ToList();

            return list;
        }

        private string GetUrlFromMedia(JObject media)
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            return media.Property("url").Value.ToString();
        }
    }
}