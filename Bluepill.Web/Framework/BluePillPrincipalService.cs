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
        //private IBluePillUserStore _userStore;
        private ICookieGateway _cookieGateway;
        private ITokenStorage _storage;
        private IBluepillUserStorage _user;

        public BluePillPrincipalService(/*IBluePillUserStore userStore,*/ ICookieGateway cookieGateway, ITokenStorage storage, IBluepillUserStorage user)
        {
            //_userStore = userStore;
            _cookieGateway = cookieGateway;
            _storage = storage;
            _user = user;
        }

        public IPrincipal GetPrincipal()
        {
            var identity = new BluePillIdentity();
            var roles = new List<string>();
            var authenticationValue = _cookieGateway.GetVale(new HttpContextWrapper(HttpContext.Current), FormsAuthentication.FormsCookieName, 0);

            if (authenticationValue != null)
            {
                var ticket = FormsAuthentication.Decrypt(authenticationValue);

                identity.IsAuthenticated = !(ticket.Expiration < DateTime.Now);
                identity.Name = ticket.Name;
                identity.AuthenticationType = "FormsAuthentication";
                                
                //var user = _userStore.GetUser(identity.Name);

                //if(user != null)
                //    identity.Collections = user.Collections;

                identity.AccessToken = _storage.GetToken(identity.Name);

                identity.Facets = _user.GetUser(identity.Name).Facets;
                   

            }

            return new GenericPrincipal(identity, roles.ToArray());
        }


    }
}