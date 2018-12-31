using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Icarus.Engine.Framework.Modding;
using Icarus.Engine.Presenters.Ships;

namespace Icarus.Engine.Framework.Spawning
{
    public static class Spawner
    {
        private static Dictionary<string, Blueprint> Blueprints { get; set; }

        public static void Initialize(IEnumerable<Blueprint> blueprints)
        {
            Blueprints = new Dictionary<string, Blueprint>(blueprints.ToDictionary(x => x.Id, x => x),
                StringComparer.OrdinalIgnoreCase);
        }

        public static IShipPresenter TestSpawn()
        {
            return (IShipPresenter) Blueprints.First().Value.Factory.Invoke();
        }
    }
}