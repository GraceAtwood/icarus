using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
        public override Texture2D ReadPath(string path)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(FindFile(path).FullName));
            return tex;
        }

        public Texture2DPathConverter(List<DirectoryInfo> searchDirectories) : base(searchDirectories)
        {
        }
    }
}