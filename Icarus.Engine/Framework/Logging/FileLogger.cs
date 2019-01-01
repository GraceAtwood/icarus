using System;
using System.IO;
using UnityEngine;

namespace Icarus.Engine.Framework.Logging
{
    public class FileLogger : ILogger
    {
        private static DirectoryInfo LogsDirectory { get; } = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Logs"));

        private static FileInfo GetLogFileName(string logType) =>
            new FileInfo(Path.Combine(LogsDirectory.FullName, $"{logType}.log.{DateTime.UtcNow:yyyy-MM-dd-HH}"));

        public void Debug(string message)
        {
            var fileInfo = GetLogFileName("application");
            fileInfo.Directory?.Create();
            File.WriteAllText(fileInfo.FullName, $"[{DateTime.UtcNow:O}][DEBUG]{message}");
        }
    }
}