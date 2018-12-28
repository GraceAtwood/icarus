using System.IO;
using UnityEngine;

namespace Icarus.Engine.Framework.Serialization.Converters
{
    /// <summary>
    /// Loads a mesh from a file path.
    /// </summary>
    public class MeshPathConverter : PathConverterBase<Mesh>
    {
        protected override Mesh ReadPath(string path)
        {
            return ObjImporter.ImportFile(path);
        }

        public MeshPathConverter(DirectoryInfo modDirectory) : base(modDirectory)
        {
        }
    }
}