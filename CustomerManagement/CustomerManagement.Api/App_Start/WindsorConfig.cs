using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CustomerManagement.Api.Windsor;

namespace CustomerManagement.Api
{
    public static class WindsorConfig
    {
        public static WindsorContainer Register(HttpConfiguration config)
        {
            var container = new WindsorContainer();
            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
            container.Install(FromAssembly.This());

            return container;
        }
    }
}