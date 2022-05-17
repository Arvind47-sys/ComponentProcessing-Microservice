using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Logger
{
    /// <summary>
    /// Log filter to catch all the possible exceptions in the application.
    /// </summary>
    public class LogFilterAttribute : ExceptionFilterAttribute
    {
        ILog logger;

        public LogFilterAttribute()
        {
            logger = LogManager.GetLogger(typeof(LogFilterAttribute));
        }

        public override void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception.Message + " - " + context.Exception.StackTrace);
        }
    }
}
