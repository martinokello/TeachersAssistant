using System.Web.Mvc;

namespace TeachersAssistant.Areas.StatePrimary
{
    public class StatePrimaryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StatePrimary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StatePrimary_default",
                "StatePrimary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                namespaces: new[] { "TeacherAssistant.Areas.StatePrimary.Controllers" }
            );
        }
    }
}