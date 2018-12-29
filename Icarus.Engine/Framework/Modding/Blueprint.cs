using System.Collections.Generic;
using System.Dynamic;

namespace Icarus.Engine.Framework.Modding
{
    public class Blueprint
    {
        public Dictionary<string, object> Data { get; }

        public List<BlueprintSourceInfo> SourceInfos { get; }

        public Blueprint(Dictionary<string, object> data, List<BlueprintSourceInfo> sourceInfos)
        {
            Data = data;
            SourceInfos = sourceInfos;
        }
    }
}