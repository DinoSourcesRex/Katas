using System.Threading.Tasks;
using System.Web.Http;
using Castle.Windsor;
using CustomerManagement.Api;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CustomerManagement.Api
{
    public class NoServerHeader : OwinMiddleware
    {
        public NoServerHeader(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            context.Response.Headers.Remove("Server");
            await Next.Invoke(context);
        }
    }

    public class Startup
    {
        public WindsorContainer Container { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            WebApiConfig.Register(httpConfiguration);
            SwaggerConfig.Register(httpConfiguration);
            LoggerConfig.Register();
            Container = WindsorConfig.Register(httpConfiguration);

            app.Use(typeof(NoServerHeader));
            app.UseWebApi(httpConfiguration);
        }
    }
}