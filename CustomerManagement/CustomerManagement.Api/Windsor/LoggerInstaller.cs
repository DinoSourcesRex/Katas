using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Serilog;

namespace CustomerManagement.Api.Windsor
{
    public class LoggerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILogger>()
                .UsingFactoryMethod((kernel, componentModel, creationContext) =>
                    Log.ForContext(creationContext.Handler.ComponentModel.Implementation))
                .LifestyleTransient());
        }
    }
}