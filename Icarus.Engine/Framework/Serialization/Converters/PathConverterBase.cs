using System;
using System.IO;
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
    public abstract class PathConverterBase<T> : JsonConverter where T : class
    {
        private DirectoryInfo ModDirectory { get; }
        
        protected PathConverterBase(DirectoryInfo modDirectory)
        {
            ModDirectory = modDirectory;
        }

        /// <inheritdoc />
        public override bool CanWrite { get; } = false;

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return typeof(T) == objectType;
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var originalPath = reader.Value as string;

            if (String.IsNullOrWhiteSpace(originalPath))
                throw new SerializationException($"Failed to load path for {originalPath}");

            var path = Path.Combine(ModDirectory.FullName, originalPath);

            if (!File.Exists(path))
                throw new SerializationException($"Failed to find a file at {path}");

            Log.Debug($"Loading item at {path}");

            return ReadPath(path);
        }

        /// <summary>
        /// Reads the path as <typeparamref name="T"/>.  A file is guaranteed to exist at the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected abstract T ReadPath(string path);
    }
}