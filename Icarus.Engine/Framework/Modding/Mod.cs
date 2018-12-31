using System;
using System.IO;
using JetBrains.Annotations;
using Icarus.Utilities;

namespace Icarus.Engine.Framework.Modding
{
    /// <summary>
    /// Represents the information about a mod.
    /// </summary>
    public class Mod : IEquatable<Mod>
    {
        [NotNull]
        public ModInfo ModInfo { get; }

        /// <summary>
        /// The directory where this mod is found.
        /// </summary>
        [NotNull]
        public DirectoryInfo Directory { get; }

        /// <summary>
        /// The size of the mod in bytes.
        /// </summary>
        public long Size { get; }

        internal Mod([NotNull] ModInfo modInfo, [NotNull] DirectoryInfo directoryInfo, long size)
        {
            ModInfo = modInfo;
            Directory = directoryInfo;
            Size = size;
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