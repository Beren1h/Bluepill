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
        private IBluePillUserStore _userStore;

        public BluePillPrincipalService(IBluePillUserStore userStore)
        {
            _userStore = userStore;
        }

        public IPrincipal GetPrincipal()
        {
            var cookies = HttpContext.Current.Request.Cookies;
            
            var identity = new BluePillIdentity();
            var roles = new List<string>();

            var authenticationCookie = cookies[FormsAuthentication.FormsCookieName];
            string authenticationValue = "";
            FormsAuthenticationTicket ticket;

            if (authenticationCookie != null)
            {
                authenticationValue = authenticationCookie.Value;
                ticket = FormsAuthentication.Decrypt(authenticationValue);

                identity.IsAuthenticated = !(ticket.Expiration < DateTime.Now);
                identity.Name = ticket.Name;
                identity.AuthenticationType = "FormsAuthentication";

                var user = _userStore.GetUser(identity.Name);

                if(user != null)
                    identity.Collections = user.Collections;
            }

            return new GenericPrincipal(identity, roles.ToArray());
        }


    }
}