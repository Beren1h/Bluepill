using System.Web.Mvc;

namespace Bluepill.Web.Areas.layout
{
    public class layoutAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "layout";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "layout_default",
                "layout/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
