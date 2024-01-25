using System.Diagnostics;

namespace DebugLogger
{
    public static class Logger
    {
        private const string Debug = "DEBUG";

        [Conditional(Debug)]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        [Conditional(Debug)]
        public static void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        [Conditional(Debug)]
        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        /// <summary>
        /// Logs error. Also happens on release builds.
        /// </summary>
        public static void Critical(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}