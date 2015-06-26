using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace WrightsAtHome.Server.API.Common
{
    public class GlobalExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            if (context != null && context.Exception != null)
            {
                var logger = context.Exception.Source != null
                    ? LogManager.GetLogger(context.Exception.Source)
                    : LogManager.GetCurrentClassLogger();

                logger.Error(context.Exception, context.Exception.Message);
            }

            return Task.FromResult(true);
        }
    }
}