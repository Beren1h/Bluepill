using Bluepill.Dropbox;
using Bluepill.Storage;
using Bluepill.Storage.StorageTypes;
using Bluepill.Web.Areas.Application.Models;
using Bluepill.Web.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.Application.Controllers
{
    public class DropboxController : Controller
    {
        private IApiRequest _dropbox;
        private ITokenStorage _storage;

        public DropboxController(IApiRequest dropbox, ITokenStorage storage)
        {
            _dropbox = dropbox;
            _storage = storage;
        }

        public async Task<ActionResult> Index()
        {
            var token = await _dropbox.GetToken(new Uri(Api.REQUEST_TOKEN), "", "");

            var model = new DropboxModel
            {
                AuthorizationToken = token[Keys.TOKEN],
                AuthorizationSecret = token[Keys.SECRET],
                AuthorizationUrl = string.Format(Api.AUTHORIZATION_URL, token[Keys.TOKEN])
            };

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            return View(model);
        }

        public async Task<ActionResult> GetAccessToken(DropboxModel model)
        {
            var uri = new Uri(Api.ACCESS_TOKEN);
            var token = await _dropbox.GetToken(uri, model.AuthorizationToken, model.AuthorizationSecret);

            model.AccessToken = token[Keys.TOKEN];
            model.AccessSecret = token[Keys.SECRET];

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            _storage.AddToken(new Token { UserId = identity.Name, Value = token[Keys.TOKEN], Secret = token[Keys.SECRET] });
            
            return View("Index", model);
        }

        public async Task<JObject> GetMetaData(DropboxModel model)
        {
            //var uri = new Uri(Api.METADATA);
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var content = await _dropbox.GetMetaData(identity.AccessToken);

            //var json = from item in content["contents"] select JObject.Parse(item.ToString());

            //var list = (from item in json.Where(x => x.Property("mime_type") != null &&
            //                x.Property("mime_type").Value.ToString() == "image/jpeg")
            //                select item.Property("path").Value.ToString()).ToList();

            return content;
        }

    }
}
