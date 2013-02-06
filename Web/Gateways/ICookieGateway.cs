using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Gateways
{
    public interface ICookieGateway
    {
        HttpCookie GetCookie(string name, HttpContextBase context);
    }
}