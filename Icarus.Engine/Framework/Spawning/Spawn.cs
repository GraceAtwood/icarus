using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Icarus.Engine.Framework.Exceptions;
using Icarus.Engine.Framework.Modding;
using Icarus.Engine.Presenters.Ships;
using Icarus.Utilities;

namespace Icarus.Engine.Framework.Spawning
{
    public static class Spawner
    {
        private static Dictionary<string, Blueprint> BlueprintsById { get; } = new Dictionary<string, Blueprint>();

        private static Dictionary<Type, Dictionary<string, Blueprint>> BlueprintsByType { get; } =
            new Dictionary<Type, Dictionary<string, Blueprint>>();

        public static void Initialize(IEnumerable<Blueprint> source)
        {
            foreach (var blueprint in source)
            {
                BlueprintsById.Add(blueprint.Id, blueprint);
                foreach (var parentType in blueprint.Class.GetAllParentTypes())
                {
                    if (BlueprintsByType.TryGetValue(parentType, out var group))
                    {
                        group.Add(blueprint.Id, blueprint);
                    }
                    else
                    {
                        BlueprintsByType.Add(parentType, new Dictionary<string, Blueprint>
                        {
                            {blueprint.Id, blueprint}
                        });
                    }
                }
            }
        }

        public static T Spawn<T>(string blueprintId) where T : ITemplateSpawnable
        {
            if (BlueprintsByType.TryGetValue(typeof(T), out var group) && group.TryGetValue(blueprintId, out var blueprint) &&
                blueprint.Factory.Invoke() is T result)
                return result;
            
            throw new BlueprintNotFoundException($"blueprint not found for id {blueprintId}");
        }
    }
}