using System;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin;
using Swashbuckle.Application;

namespace CustomerManagement.Api
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            httpConfiguration
                .EnableSwagger(c =>
                {
                    c.RootUrl(OwinRootUrlResolver);
                    c.SingleApiVersion("v1", "Payment Provider API");
                })
                .EnableSwaggerUi(c =>
                {
                    c.DisableValidator();
                    c.DocExpansion(DocExpansion.List);
                });
        }

        private static string OwinRootUrlResolver(HttpRequestMessage request)
        {
            IOwinContext owinContext = request.GetOwinContext();
            string requestPathBase = (string)owinContext.Environment["owin.RequestPathBase"];
            Uri authority = new Uri(request.RequestUri.GetLeftPart(UriPartial.Authority));

            return new Uri(authority, requestPathBase).ToString();
        }
    }
}