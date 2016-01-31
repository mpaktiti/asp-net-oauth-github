using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GithubOAuth.Startup))]
namespace GithubOAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
