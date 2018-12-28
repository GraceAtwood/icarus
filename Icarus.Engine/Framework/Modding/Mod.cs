using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using Icarus.Engine.Framework.Serialization.Converters;
using Icarus.Engine.Utilities;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Icarus.Engine.Framework.Modding
{
    /// <summary>
    /// Represents the information about a mod.
    /// </summary>
    public class Mod : IEquatable<Mod>
    {
        [CanBeNull] public ModInfo ModInfo { get; }

        /// <summary>
        /// The directory where this mod is found.
        /// </summary>
        [NotNull]
        public DirectoryInfo Directory { get; }

        /// <summary>
        /// The assemblies that were loaded in this mod.
        /// </summary>
        public List<Assembly> Assemblies { get; }

        /// <summary>
        /// The templates that were loaded in this mod.
        /// </summary>
        public List<ExpandoObject> Templates { get; }

        /// <summary>
        /// The size of the mod in bytes.
        /// </summary>
        public long Size { get; }

        internal Mod([CanBeNull] ModInfo modInfo, [NotNull] DirectoryInfo directoryInfo, long size,
            List<Assembly> assemblies, List<ExpandoObject> templates)
        {
            ModInfo = modInfo;
            Directory = directoryInfo;
            Size = size;
            Assemblies = assemblies;
            Templates = templates;
        }

        internal Mod([NotNull] DirectoryInfo directoryInfo, long totalBytes, List<Assembly> assemblies,
            List<ExpandoObject> templates)
            : this(null, directoryInfo, totalBytes, assemblies, templates)
        {
        }

        /// <inheritdoc />
        bool IEquatable<Mod>.Equals(Mod other)
        {
            return other?.Directory == Directory;
        }

        /// <summary>
        /// Compares the equality of two mods.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.FullEquals(obj);
        }

        /// <summary>
        /// Compares two mods.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Mod x, Mod y)
        {
            return ReferenceEquals(x, y) || x != null && x.Equals(y);
        }

        /// <summary>
        /// Compares two definitions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Mod x, Mod y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Returns the hash code of the directory this mod was found in.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Directory.GetHashCode();
        }
    }
}