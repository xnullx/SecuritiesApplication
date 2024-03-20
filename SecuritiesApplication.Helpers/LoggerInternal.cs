using Microsoft.Extensions.Logging;

namespace SecuritiesApplication.Helpers
{
    public class LoggerInternal : ILoggerInternal
    {
        private readonly ILogger _logger;

        public LoggerInternal(ILogger logger)
        {
            _logger = logger;
        }
        public void LogError(string message, Exception? exception = null)
        {
            _logger.LogError(message, exception);
        }

        public void LogWarn(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
