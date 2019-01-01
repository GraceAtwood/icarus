using System.Collections.Generic;

namespace Icarus.Engine.Framework.Logging
{
    public static class Log
    {
        private static List<ILogger> Loggers { get; set; } = new List<ILogger>
        {
            new UnityLogger(),
            new FileLogger()
        };

        public static void RegisterLoggers(params ILogger[] loggers)
        {
            RegisterLoggers((IEnumerable<ILogger>) loggers);
        }

        public static void RegisterLoggers(IEnumerable<ILogger> loggers)
        {
            foreach (var logger in loggers)
            {
                Loggers.Add(logger);
            }
        }

        public static void Debug(string message)
        {
            foreach (var logger in Loggers)
            {
                logger.Debug(message);
            }
        }
    }
}