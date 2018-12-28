using System;
using UnityEngine;

namespace Icarus.Engine.Framework.Modding
{
    /// <summary>
    /// The base mod info.  Intended to be deserialized from the mod info file.
    /// </summary>
    public class ModInfo
    {
        /// <summary>
        /// The name of the mod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The author of this mod.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// A website or other location at which more information about this mod can be found.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// A description of this mod.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An icon for this mod.
        /// </summary>
        public Texture2D Icon { get; set; }

        /// <summary>
        /// The version of this mod in a "Major.Minor.Build.Revision".  Eg: 1.0.1.4
        /// </summary>
        public Version Version { get; set; } = new Version("1.0.0.0");

        /// <summary>
        /// Determines if the mod should be loaded or not.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}