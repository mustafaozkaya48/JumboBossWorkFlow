using System.Web.Mvc;

namespace JumboBossWorkFlow.Areas.wfpanel
{
    public class WfpanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "wfpanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "wfpanel_default",
                "wfpanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}