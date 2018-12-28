namespace Icarus.Engine.Framework.Logging
{
    public class UnityLogger : ILogger
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}