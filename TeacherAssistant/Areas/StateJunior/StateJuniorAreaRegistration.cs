using System.Web.Mvc;

namespace TeachersAssistant.Areas.StateJunior
{
    public class StateJuniorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StateJunior";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StateJunior_default",
                "StateJunior/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                namespaces: new[] { "TeacherAssistant.Areas.StateJunior.Controllers" }
            );
        }
    }
}