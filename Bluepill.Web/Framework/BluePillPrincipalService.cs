using Bluepill.Search;
using Bluepill.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Bluepill.Web.Framework
{
    public class BluePillPrincipalService : IPrincipalService
    {
        private ITokenStorage _storage;
        private IBluepillUserStorage _user;

        public BluePillPrincipalService(ITokenStorage storage, IBluepillUserStorage user)
        {
            _storage = storage;
            _user = user;
        }

        public IPrincipal GetPrincipal()
        {
            var identity = new BluePillIdentity();
            var roles = new List<string>();
            var authenticationCookie = new HttpContextWrapper(HttpContext.Current).Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authenticationCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authenticationCookie.Value);

                identity.IsAuthenticated = !(ticket.Expiration < DateTime.Now);
                identity.Name = ticket.Name;
                identity.AuthenticationType = "FormsAuthentication";
                identity.AccessToken = _storage.GetToken(identity.Name);
                identity.Facets = _user.GetUser(identity.Name).Facets;
                identity.IsMobile = new HttpContextWrapper(HttpContext.Current).Request.Browser.IsMobileDevice;
                //identity.IsMobile = true;
            }

            return new GenericPrincipal(identity, roles.ToArray());
        }

    }
}