using System.Web.Mvc;

namespace TeachersAssistant.Areas.Grammar11Plus
{
    public class Grammar11PlusAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Grammar11Plus";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Grammar11Plus_default",
                "Grammar11Plus/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },null, 
                namespaces: new[] { "TeacherAssistant.Areas.Grammar11Plus.Controllers" }
            );
        }
    }
}