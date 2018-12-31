using System;
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
    public abstract class PathConverterBase<T> : JsonConverter<T> where T : class
    {
        private List<DirectoryInfo> SearchDirectories { get; }
        
        protected PathConverterBase(List<DirectoryInfo> searchDirectories)
        {
            SearchDirectories = searchDirectories;
        }

        protected FileInfo FindFile(string path)
        {
            foreach (var searchDirectory in SearchDirectories)
            {
                var candidatePath = Path.Combine(searchDirectory.FullName, path);
                if (File.Exists(candidatePath))
                    return new FileInfo(candidatePath);
            }

            return null;
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var path = reader.ReadAsString();
            return ReadPath(path);
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the path as <typeparamref name="T"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public abstract T ReadPath(string path);
    }
}