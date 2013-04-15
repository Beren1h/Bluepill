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

        public DropboxController(ITokenStorage storage, IApiRequest dropbox)
        {
            _dropbox = dropbox;
            _storage = storage;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var token = await _dropbox.RequestAuthorizationToken();

            var model = new DropboxModel
            {
                AuthorizationToken = token[Keys.TOKEN],
                AuthorizationSecret = token[Keys.SECRET],
                AuthorizationUrl = _dropbox.GetAuthorizationUrl(token[Keys.TOKEN], token[Keys.SECRET])
            };

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> GetAccessToken(DropboxModel model)
        {
            var token = await _dropbox.RequestAccessToken(model.AuthorizationToken, model.AuthorizationSecret);

            model.AccessToken = token[Keys.TOKEN];
            model.AccessSecret = token[Keys.SECRET];

            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            _storage.AddToken(new Token { UserId = identity.Name, Value = token[Keys.TOKEN], Secret = token[Keys.SECRET] });

            return View("Index", model);
        }

        [AllowAnonymous]
        public async Task<JObject> GetMetaData(DropboxModel model)
        {
            var identity = (BluePillIdentity)ControllerContext.HttpContext.User.Identity;
            var content = await _dropbox.GetMetaData(identity.AccessToken);

            return content;
        }

    }
}
