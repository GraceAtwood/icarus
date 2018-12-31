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
    internal class Texture2DPathConverter : CacheablePathConverterBase<Texture2D>
    {
        /// <inheritdoc />
        protected override Texture2D Load(FileInfo file)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(file.FullName));
            return tex;
        }

        public Texture2DPathConverter(List<DirectoryInfo> searchDirectories) : base(searchDirectories)
        {
        }
    }
}