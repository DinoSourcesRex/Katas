using System.Web.Http.ExceptionHandling;

namespace CustomerManagement.Api.Logging
{
    public class DomainExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            if (context.ExceptionContext?.ControllerContext?.Controller != null)
            {
                var logger = Serilog.Log.ForContext(context.ExceptionContext.ControllerContext.Controller.GetType());
                logger.Error(context.Exception, context.Exception.Message);
                return;
            }

            Serilog.Log.Logger.Error(context.Exception, context.Exception.Message);
        }

        //Default implementation of ShouldLog is fired twice if not overridden
        //implementation. Ugh.

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            const string requestKey = "PaymentProviderExceptionLogger.Logged";

            if (context.Request.Properties.ContainsKey(requestKey))
            {
                return false;
            }

            context.Request.Properties.Add(requestKey, true);
            return true;
        }
    }
}