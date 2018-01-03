using Microsoft.Owin;
using Owin;
using System.Web.Security;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(TeacherAssistant.Startup))]
namespace  TeacherAssistant 
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
        }
    }
}
