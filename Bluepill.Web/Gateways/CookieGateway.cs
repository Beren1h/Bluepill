using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Gateways
{
    public class CookieGateway : ICookieGateway
    {
        public HttpCookie GetCookie(string name, HttpContextBase context)
        {
            return context.Request.Cookies[name];
        }
    }
}