using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Icarus.Engine.Framework.Logging;
using Newtonsoft.Json;

namespace Icarus.Engine.Framework.Serialization.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Loads an object from a file path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CacheablePathConverterBase<T> : JsonConverter<T> where T : class
    {
        private List<DirectoryInfo> SearchDirectories { get; }

        private static ConcurrentDictionary<FileInfo, T> Cache { get; } = new ConcurrentDictionary<FileInfo, T>();

        protected CacheablePathConverterBase(List<DirectoryInfo> searchDirectories)
        {
            SearchDirectories = searchDirectories;
        }

        private FileInfo FindFile(string relativePath)
        {
            foreach (var searchDirectory in SearchDirectories)
            {
                var candidatePath = Path.Combine(searchDirectory.FullName, relativePath);
                if (File.Exists(candidatePath))
                    return new FileInfo(candidatePath);
            }
            
            return null;
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var relativePath = reader.ReadAsString();
            return LoadWithCache(relativePath);
        }

        public T LoadWithCache(string relativePath)
        {
            var fullPath = FindFile(relativePath);
            return Cache.GetOrAdd(fullPath, Load);
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the path as <typeparamref name="T"/>.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected abstract T Load(FileInfo file);
    }
}