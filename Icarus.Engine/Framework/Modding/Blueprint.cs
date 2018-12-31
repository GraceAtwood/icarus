using System;
using System.Collections.Generic;

namespace Icarus.Engine.Framework.Modding
{
    public class Blueprint
    {
        public Dictionary<string, object> Data { get; set; }

        public List<BlueprintSourceInfo> SourceInfos { get; set; }

        public Func<object> Factory { get; set; }

        public string Id { get; set;  }

        public Type Class { get; set;  }
    }
}