using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Runtime.Remoting.Contexts;
using GuildCars.Datas.DBContext;
using Microsoft.AspNet.Identity.Owin;

namespace GuildCars.Datas.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var options = new CookieAuthenticationOptions();
            //options.AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active;
            //options.AuthenticationType = "ApplicationCookie";
            //options.LoginPath = new PathString("/auth/login");

            //app.UseCookieAuthentication(options);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login"),
            });
            app.CreatePerOwinContext(() => new  CarDBContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>((options, context) => new UserManager<IdentityUser>(new UserStore<IdentityUser>(context.Get<CarDBContext>())));
            app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<CarDBContext>())));
        }
    }
}
