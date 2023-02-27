using Microsoft.Extensions.Logging;

namespace Preezie.UnitTests.TestDoubles
{
    public class LogEntry
    {
        public EventId EventId { get; set; }

        public Exception Exception { get; set; }

        public LogLevel LogLevel { get; set; }

        public object State { get; set; }
    }

    public class LoggerStub<T> : ILogger<T>
    {
        public List<LogEntry> LogEntries = new List<LogEntry>();

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogEntries.Add(new LogEntry
            {
                LogLevel = logLevel,
                EventId = eventId,
                State = state,
                Exception = exception
            });

            return;
        }
    }
}