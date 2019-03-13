using System.Web.Mvc;

namespace TeachersAssistant.Areas.Grammar11Plus
{
    public class SecondarySchoolAreaRegistration : AreaRegistration 
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
                "SecondarySchool_default",
                "SecondarySchool/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },null, 
                namespaces: new[] { "TeacherAssistant.Areas.SecondarySchool.Controllers" }
            );
        }
    }
}