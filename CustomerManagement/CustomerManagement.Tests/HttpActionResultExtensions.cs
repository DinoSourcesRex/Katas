using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace CustomerManagement.Tests
{
    public static class HttpActionResultExtensions
    {
        public static HttpStatusCode StatusCode(this IHttpActionResult instance)
        {
            var response = instance.ExecuteAsync(new CancellationToken()).Result;
            return response.StatusCode;
        }

        public static T Content<T>(this IHttpActionResult instance)
        {
            var response = instance.ExecuteAsync(new CancellationToken()).Result;
            return response.Content.ReadAsAsync<T>().Result;
        }
    }
}