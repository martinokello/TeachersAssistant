using System.Web.Mvc;

namespace TeachersAssistant.Areas.Grammar11Plus
{
    public class CollegeAndPostGraduateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CollegeAndPostGraduate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CollegeAndPostGraduate_default",
                "CollegeAndPostGraduate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },null, 
                namespaces: new[] { "TeacherAssistant.Areas.CollegeAndPostGraduate.Controllers" }
            );
        }
    }
}