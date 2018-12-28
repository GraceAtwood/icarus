using System.IO;
using UnityEngine;

namespace Icarus.Engine.Framework.Serialization.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Loads a texture2d from a file path.
    /// </summary>
    internal class Texture2DPathConverter : PathConverterBase<Texture2D>
    {
        /// <inheritdoc />
        protected override Texture2D ReadPath(string path)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(path));
            return tex;
        }

        public Texture2DPathConverter(DirectoryInfo modDirectory) : base(modDirectory)
        {
        }
    }
}