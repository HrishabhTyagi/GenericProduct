using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace MigrationToolApi.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly Logger logger;

        public CustomExceptionFilter()
        {
            logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        private Exception GetOriginalException(Exception ex)
        {

            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            LogEventInfo logEvent = new LogEventInfo(NLog.LogLevel.Error, null, null, exception.Message, null, exception);

            logEvent.Properties["error-source"] = exception.Source;
            logEvent.Properties["error-stacktrace"] = exception.StackTrace;
            logEvent.Properties["error-controller"] = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            logEvent.Properties["error-action"] = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;

            if (exception.InnerException != null)
            {
                var innerException = GetOriginalException(exception);
                logEvent.Properties["inner-error-message"] = innerException.Message;
            }

            logger.Log(logEvent);
        }
    }
}