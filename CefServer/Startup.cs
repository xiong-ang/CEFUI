using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Beginor.Owin.StaticFile;
using System.IO;
using System;

[assembly: OwinStartup(typeof(CefServer.Startup))]

namespace CefServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.UseStaticFile(new StaticFileMiddlewareOptions() {
                RootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UI")
            });
        }
    }
}
