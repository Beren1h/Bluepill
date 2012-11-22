using System.Web.Mvc;

namespace Bluepill.Web.Areas.Bluepill
{
    public class BluepillAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Bluepill";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Bluepill_default",
                "Bluepill/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
