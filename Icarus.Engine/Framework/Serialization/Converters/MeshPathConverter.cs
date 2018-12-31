using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Icarus.Engine.Framework.Serialization.Converters
{
    /// <summary>
    /// Loads a mesh from a file path.
    /// </summary>
    public class MeshPathConverter : PathConverterBase<Mesh>
    {
        public override Mesh ReadPath(string path)
        {
            return ObjImporter.ImportFile(FindFile(path).FullName);
        }

        public MeshPathConverter(List<DirectoryInfo> searchDirectories) : base(searchDirectories)
        {
        }
    }
}