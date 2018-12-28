using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using Icarus.Engine.Framework.Serialization.Converters;
using Icarus.Engine.Utilities;
using JetBrains.Annotations;
using Newtonsoft.Json;
using static Icarus.Engine.Framework.Logging.Log;

namespace Icarus.Engine.Framework.Modding
{
    public static class ModLoader
    {
        public static List<Mod> LoadMods(DirectoryInfo modsDirectoryInfo)
        {
            Debug($"Started loading mods from {modsDirectoryInfo.FullName}...");

            var mods = new List<Mod>();

            foreach (var directory in modsDirectoryInfo.EnumerateDirectories())
            {
                var modInfo = GetModInfoFromDirectory(directory);
                
                if (modInfo?.IsActive == false)
                    continue;
                
                var assemblies = directory.EnumerateFilesRecursively("*.dll")
                    .Select(assemblyFile => Assembly.LoadFrom(assemblyFile.FullName))
                    .ToList();
                var templates = directory.EnumerateFilesRecursively("*.json").Where(file =>
                        ModInfoNames.None(name =>
                            file.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)))
                    .Select(file => JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(file.FullName)))
                    .ToList();


                mods.Add(new Mod(modInfo, directory, directory.GetSize(), assemblies, templates));
            }

            Debug($"Finished loading mods.");
            return mods;
        }

        private static readonly string[] ModInfoNames = {"modinfo", "info", "config", "mod"};

        [CanBeNull]
        private static ModInfo GetModInfoFromDirectory(DirectoryInfo directoryInfo)
        {
            var modInfoFile = directoryInfo.EnumerateFilesRecursively()
                .FirstOrDefault(file =>
                    ModInfoNames.Any(name => file.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)));

            if (modInfoFile == null)
                return null;

            Debug($"Found info file: {modInfoFile.FullName}!  Loading it now...");
            var modInfo = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modInfoFile.FullName),
                new Texture2DPathConverter(directoryInfo));
            Debug($"Loaded {modInfoFile.Name} {directoryInfo.Name} directory for mod '{modInfo.Name}'");

            return modInfo;
        }
    }
}