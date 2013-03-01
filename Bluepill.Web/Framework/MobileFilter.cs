using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Framework
{
    public class MobileFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var isMobile = context.RequestContext.HttpContext.Request.Browser.IsMobileDevice;
            var identity = (BluePillIdentity)context.RequestContext.HttpContext.User.Identity;


            if (isMobile)
            {
                identity.IsMobile = true;
            }


            base.OnResultExecuting(context);
        }
    }
}