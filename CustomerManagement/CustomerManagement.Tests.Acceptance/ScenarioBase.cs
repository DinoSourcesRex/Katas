using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CustomerManagement.Api;
using CustomerManagement.Api.Repositories;
using Microsoft.Owin.Testing;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace CustomerManagement.Tests.Acceptance
{
    public class ScenarioBase
    {
        private TestServer _server;
        private WindsorContainer _windsorContainer;

        [BeforeScenario(Order = 0)]
        public void Start()
        {
            _server = TestServer.Create(app =>
            {
                var startup = new Startup();
                startup.Configuration(app);
                _windsorContainer = startup.Container;
            });

            //server.HttpClient returns a new HttpClient each time through
            //the "get" property unfortunately we need one httpclient to 
            //add the default header to for the acceptance tests. Ugh!

            var httpClient = _server.HttpClient;

            Rebind<ISqlConnectionFactory>(new RepositoryConnectionFactory(FeatureContext.Current["Title"].ToString()));
        }

        [After]
        public void Stop()
        {
            if (_server != null)
            {
                _server.Dispose();
                _server = null;
            }
        }

        /// <summary>
        /// Initiates a rebind for the DI
        /// 
        /// Usage:
        ///     Mock<TService>();
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Mock<TService>() where TService : class
        {
            TService service = MockRepository.GenerateMock<TService>();
            Rebind(service);
            return service;
        }

        public void Rebind<TService>(TService service) where TService : class
        {
            _windsorContainer.Register(Component.For<TService>().Instance(service).IsDefault().LifestyleTransient());
        }
    }
}