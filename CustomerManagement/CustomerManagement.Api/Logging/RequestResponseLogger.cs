using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Serilog;

namespace CustomerManagement.Api.Logging
{
    public class RequestResponseLogger : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var logger = Log.ForContext(actionContext.ControllerContext.Controller.GetType());
            LogRequest(logger, actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                var logger = Log.ForContext(actionExecutedContext.ActionContext.ControllerContext.Controller.GetType());
                LogResponse(logger, actionExecutedContext.Response);
            }
        }

        private static void LogRequest(ILogger logger, HttpActionContext context)
        {
            var request = context.Request;

            var requestBody = context.ActionArguments
                .Select(a => a.Value)
                .FirstOrDefault();

            logger.Debug("Request: {method} {requestUri} - {@content}", request.Method, request.RequestUri, requestBody);
        }

        private static void LogResponse(ILogger logger, HttpResponseMessage response)
        {
            logger.Debug("Response: {statusCode}", response.StatusCode);
        }
    }
}