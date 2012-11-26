using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public interface ICookieGateway
    {
        string GetVale( HttpContextBase context, string cookieName, dynamic key);
    }
}