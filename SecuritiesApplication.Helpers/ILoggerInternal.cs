namespace SecuritiesApplication.Helpers
{
    public interface ILoggerInternal
    {
        void LogWarn(string message);
        void LogError(string message,Exception? ex = null);
    }
}
