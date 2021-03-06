﻿using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Deparcq.Common.Json
{
    public static class SerializationExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings;

        static SerializationExtensions()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.Converters.Add(new StringEnumConverter());
            JsonSerializerSettings.Converters.Add(new LocalDateJsonConverter());
            JsonSerializerSettings.ContractResolver = new PrivateSetterContractResolver();
            JsonSerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
        }

        public static string Serialize(this object o, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(o, formatting, JsonSerializerSettings);
        }

        public static void Serialize(this object o, StreamWriter writer)
        {
            var serializer = JsonSerializer.Create(JsonSerializerSettings);
            serializer.Serialize(writer, o);
        }

        public static T Deserialize<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s, JsonSerializerSettings);
        }

        public static T Deserialize<T>(this StreamReader reader)
        {
            var serializer = JsonSerializer.Create(JsonSerializerSettings);
            return serializer.Deserialize<T>(new JsonTextReader(reader));
        }

        public static T Cast<T>(this object o)
        {
            return Deserialize<T>(o.Serialize());
        }
    }
}
