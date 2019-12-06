using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(OwinApp.Startup))]

namespace OwinApp
{
    #region snippet
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOpenIdConnectAuthentication(
                 new OpenIdConnectAuthenticationOptions
                 {
                     // … Your preexisting options … 
                     CookieManager = new SameSiteCookieManager(
                                         new SystemWebCookieManager())
                 });

            // Remaining code removed for brevity.
            #endregion

            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello, world.");
            });
        }
    }
}

// Install-Package Microsoft.Owin.Security.OpenIdConnect -Version 4.1.0