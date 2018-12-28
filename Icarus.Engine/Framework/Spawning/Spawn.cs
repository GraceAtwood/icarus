using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using Icarus.Engine.Framework.Exceptions;
using Icarus.Engine.Framework.Modding;

namespace Icarus.Engine.Framework.Spawning
{
    public static class Spawner
    {
        public static void Initialize(List<Mod> loadedMods)
        {
            var spawnableTypes = loadedMods
                .SelectMany(mod => mod.Assemblies.SelectMany(assembly => assembly.GetExportedTypes()))
                .Where(type => typeof(ISpawnable).IsAssignableFrom(type));

            foreach (var spawnableType in spawnableTypes)
            {
                var constructorInfos = spawnableType.GetConstructors();
                if (constructorInfos.Length != 1)
                    throw new ModIncompatibleException($"too many or no ctors in type {type.FullName}"); //TODO

                var constructor = constructorInfos.First();
                var constructorParameters = constructor.GetParameters();
            
                var args = new List<Expression>();

                foreach (var constructorParameter in constructorParameters)
                {
                    template.FirstOrDefault(x => x.Key.Equals(constructorParameter.Name))
                }
            }
        }
        
        public static T Spawn<T>(string templateId) where T : class, ISpawnable
        {
        }
    }
}