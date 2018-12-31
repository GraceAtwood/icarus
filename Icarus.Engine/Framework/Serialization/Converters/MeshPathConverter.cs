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
    public class MeshPathConverter : CacheablePathConverterBase<Mesh>
    {
        protected override Mesh Load(FileInfo file) => ObjImporter.ImportFile(file.FullName);

        public MeshPathConverter(List<DirectoryInfo> searchDirectories) : base(searchDirectories)
        {
        }
    }
}