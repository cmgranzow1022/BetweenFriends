using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetweenFriends.Startup))]
namespace BetweenFriends
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
