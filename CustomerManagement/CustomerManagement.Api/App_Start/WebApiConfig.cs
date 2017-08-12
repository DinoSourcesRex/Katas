using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CustomerManagement.Api.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CustomerManagement.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Filters.Add(new RequestResponseLogger());
            config.Services.Replace(typeof(IExceptionLogger), new DomainExceptionLogger());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = false,
                CamelCaseText = false,
            });
        }
    }
}