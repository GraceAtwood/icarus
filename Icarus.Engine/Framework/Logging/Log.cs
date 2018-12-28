namespace Icarus.Engine.Framework.Logging
{
    public static class Log
    {
        private static ILogger Logger { get; set; } = new UnityLogger();

        public static void Initialize(ILogger logger)
        {
            Logger = logger;
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }
    }
}