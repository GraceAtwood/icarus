using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Icarus.Engine.Framework.Exceptions;
using Icarus.Engine.Framework.Logging;
using Icarus.Engine.Framework.Serialization.Converters;
using Icarus.Engine.Framework.Spawning;
using Icarus.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using static Icarus.Engine.Framework.Logging.Log;

namespace Icarus.Engine.Framework.Modding
{
    public static class ModLoader
    {
        public static void LoadMods(List<Mod> mods)
        {
            var blueprints = new Dictionary<string, Blueprint>();
            var knownTypes = new Dictionary<string, Type>();

            foreach (var mod in mods)
            {
                AddTemplateSpawnableTypes(mod, knownTypes);
            }

            foreach (var mod in mods)
            {
                AddBlueprints(mod, blueprints, knownTypes);
            }

            foreach (var blueprint in blueprints.Values)
            {
                blueprint.Factory = BuildFactoryMethod(blueprint.Class, blueprint);
            }

            Spawner.Initialize(blueprints.Values);
        }

        public static List<Mod> GetMods(DirectoryInfo modDirectory) => modDirectory.EnumerateDirectories()
            .Select(info => new Mod(GetModInfoFromDirectory(info), info, info.GetSize())).ToList();

        private static readonly string[] ModInfoNames = {"modinfo", "info", "config", "mod"};

        private static ModInfo GetModInfoFromDirectory(DirectoryInfo directoryInfo)
        {
            var modInfoFile = directoryInfo.EnumerateFilesRecursively()
                .FirstOrDefault(file =>
                    ModInfoNames.Any(name => file.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)));

            if (modInfoFile == null)
                return new ModInfo
                {
                    Name = GetDefaultModName(directoryInfo)
                };

            Debug($"Found info file: {modInfoFile.FullName}!  Loading it now...");
            var modInfo = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modInfoFile.FullName),
                new Texture2DPathConverter(new List<DirectoryInfo> {directoryInfo}));
            
            Debug($"Loaded {modInfoFile.Name} {directoryInfo.Name} directory for mod '{modInfo.Name}'");

            return modInfo;
        }

        private static string GetDefaultModName(DirectoryInfo directoryInfo) => directoryInfo.Name;

        private static void AddTemplateSpawnableTypes(Mod mod, Dictionary<string, Type> knownTypes)
        {
            Debug($"Loading template spawnable types for {mod.ModInfo.Name} in directory {mod.Directory.FullName}");

            var assemblies = mod.Directory.EnumerateFilesRecursively("*.dll")
                .Select(fileInfo => Assembly.LoadFrom(fileInfo.FullName)).ToList();

            Debug($"Loaded {assemblies.Count} assemblies for {mod.ModInfo.Name}: " +
                  $"{assemblies.Select(x => x.GetName().Name).Join(", ")}");

            var templateSpawnableTypes = assemblies.SelectMany(assembly =>
                assembly.GetTypes().Where(type => typeof(ITemplateSpawnable).IsAssignableFrom(type))).ToList();

            Debug($"Found {templateSpawnableTypes.Count} template spawnable types in {mod.ModInfo.Name}: " +
                  $"{templateSpawnableTypes.Select(x => x.Name).Join(", ")}");

            foreach (var templateSpawnableType in templateSpawnableTypes)
            {
                knownTypes.Add(templateSpawnableType.Name, templateSpawnableType);
            }
        }

        private static void AddBlueprints(Mod mod,
            Dictionary<string, Blueprint> currentBlueprints, Dictionary<string, Type> knownTypes)
        {
            Debug($"Loading blueprints for {mod.ModInfo.Name}");

            var blueprintFiles = mod.Directory.EnumerateFilesRecursively("*.blueprint");

            foreach (var blueprintFile in blueprintFiles)
            {
                var rawData =
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(blueprintFile.FullName));

                var blueprintId = rawData.TryGetValue(nameof(Blueprint.Id), out var idValue)
                    ? idValue as string
                    : GetDefaultBlueprintId(mod, blueprintFile);

                if (String.IsNullOrWhiteSpace(blueprintId))
                    throw new ModIncompatibleException("blueprints must have ids!");

                if (!rawData.TryGetValue(nameof(Blueprint.Class), out var classValue))
                    throw new ModIncompatibleException("blueprints must declare a target type regex!");

                if (!knownTypes.TryGetValue((string) classValue, out var targetType))
                    throw new ModIncompatibleException("type unknown");

                if (currentBlueprints.TryGetValue(blueprintId, out var blueprint))
                {
                    blueprint.Class = targetType;

                    foreach (var pair in rawData)
                    {
                        if (blueprint.Data.ContainsKey(pair.Key))
                            blueprint.Data[pair.Key] = pair.Value;
                        else
                            blueprint.Data.Add(pair.Key, pair.Value);
                    }

                    blueprint.SourceInfos.Add(new BlueprintSourceInfo(mod, blueprintFile));
                }
                else
                {
                    currentBlueprints.Add(blueprintId, new Blueprint
                    {
                        Id = blueprintId,
                        Data = new Dictionary<string, object>(rawData, StringComparer.OrdinalIgnoreCase),
                        SourceInfos = new List<BlueprintSourceInfo> {new BlueprintSourceInfo(mod, blueprintFile)},
                        Class = targetType
                    });
                }
            }
        }
        
        private static string GetDefaultBlueprintId(Mod mod, FileInfo blueprintFile) =>
            $"{mod.ModInfo.Name}_{blueprintFile.Name}";

        private static Func<object> BuildFactoryMethod(Type type, Blueprint blueprint)
        {
            Debug($"Start building factory method for {type.FullName} with blueprint with id {blueprint.Id}");
            var createMethod = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(x => x.Name.Equals("create", StringComparison.CurrentCultureIgnoreCase));

            if (createMethod == null)
            {
                Debug("Found no create method, going to use ctor...");
                return BuildFactoryFromConstructor(type.GetConstructors().FirstOrDefault(), blueprint);
            }

            Debug("Found Create method! Using it...");
            return BuildFactoryFromCreate(createMethod, blueprint);
        }

        private static Func<object> BuildFactoryFromCreate(MethodInfo methodInfo, Blueprint blueprint)
        {
            var parameters = GetParameterValues(methodInfo, blueprint);
            return () => methodInfo.Invoke(null, parameters.Select(x => x.Invoke()).ToArray());
        }

        private static Func<object> BuildFactoryFromConstructor(ConstructorInfo constructorInfo, Blueprint blueprint)
        {
            var parameters = GetParameterValues(constructorInfo, blueprint);
            return () => constructorInfo.Invoke(parameters.Select(x => x.Invoke()).ToArray());
        }

        private static List<Func<object>> GetParameterValues(MethodBase methodBase, Blueprint blueprint)
        {
            var parameters = new List<Func<object>>();
            var methodParameters = methodBase.GetParameters();
            
            Debug($"Method base is of type {methodBase.MemberType}, declared by {methodBase.DeclaringType?.Name}, " +
                  $"and it has {methodParameters.Length} parameters.  " +
                  $"Their names in order are: {methodParameters.Select(x => x.Name).Join(", ")}");

            foreach (var parameterInfo in methodParameters)
            {
                if (blueprint.Data.TryGetValue(parameterInfo.Name, out var value))
                {
                    Debug($"Parameter type is {parameterInfo.ParameterType.Name} and value type is {value.GetType().Name}");
                    
                    if (parameterInfo.ParameterType == typeof(Mesh))
                        parameters.Add(() =>
                            new MeshPathConverter(blueprint.SourceInfos.Select(x => x.SourceMod.Directory).Reverse()
                                .ToList()).LoadWithCache(value as string));
                    else if (parameterInfo.ParameterType == typeof(Texture2D))
                        parameters.Add(() =>
                            new Texture2DPathConverter(blueprint.SourceInfos.Select(x => x.SourceMod.Directory)
                                .Reverse().ToList()).LoadWithCache(value as string));
                    else
                        parameters.Add(() => value);
                }
                else if (parameterInfo.ParameterType.IsClass && !parameterInfo.ParameterType.IsAbstract)
                {
                    parameters.Add(BuildFactoryMethod(parameterInfo.ParameterType, blueprint));
                }
            }

            return parameters;
        }
    }
}