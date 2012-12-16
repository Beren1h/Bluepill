using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Bluepill.Web.Framework
{
    public class CookieGateway : ICookieGateway
    {
        public string GetVale(HttpContextBase context, string cookieName, dynamic key)
        {
            var cookie = context.Request.Cookies[cookieName];

            if (cookie != null)
            {
                return cookie.Values[key];
            }

            return null;
        }

        public string GetKeyValue(HttpContextBase context, string cookieName, string key)
        {
            var cookie = context.Request.Cookies[cookieName];

            if (cookie != null)
            {
                return cookie.Values[key];
            }

            return null;
        }
    }
}