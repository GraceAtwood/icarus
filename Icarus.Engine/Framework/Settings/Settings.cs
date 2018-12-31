using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Icarus.Engine.Framework.Settings
{
    public abstract class Settings
    {
        private static DirectoryInfo SettingsDirectory { get; } = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Settings"));

        private static FileInfo GetSettingsFilePath<T>() =>
            new FileInfo(Path.Combine(SettingsDirectory.FullName, typeof(T).Name.ToLower()));


        public void Save<T>() where T : Settings, new()
        {
            File.WriteAllText(GetSettingsFilePath<T>().FullName, JsonConvert.SerializeObject(this));
        }

        public static void Delete<T>() where T : Settings, new()
        {
            File.Delete(GetSettingsFilePath<T>().FullName);
        }

        public static bool Exists<T>() where T : Settings, new()
        {
            return File.Exists(GetSettingsFilePath<T>().FullName);
        }

        public static T Load<T>() where T : Settings, new()
        {
            return !Exists<T>()
                ? new T()
                : JsonConvert.DeserializeObject<T>(File.ReadAllText(GetSettingsFilePath<T>().FullName));
        }
    }
}