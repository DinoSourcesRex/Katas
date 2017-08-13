using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CustomerManagement.Api.Repositories;

namespace CustomerManagement.Api.Windsor
{
    public class ConventionsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISqlConnectionFactory>()
                .ImplementedBy<SqlConnectionFactory>()
                .IsDefault());

            container.Register(Component.For<HttpContextBase>()
                .LifeStyle.PerWebRequest
                .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current)));

            container.Register(Classes.FromThisAssembly()
                .InNamespace("CustomerManagement.Api", includeSubnamespaces: true)
                .WithServiceDefaultInterfaces()
                .LifestyleTransient());
        }
    }
}