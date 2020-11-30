using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(signalRServer.Startup))]
namespace signalRServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ServerUrl = "http://*:8080";
            var webApiServer = WebApp.Start(ServerUrl);
            while (true)
            {

            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
