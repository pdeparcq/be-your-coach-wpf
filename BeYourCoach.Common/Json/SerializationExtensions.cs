using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeYourCoach.Common.Json
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
        }

        public static string Serialize(this object o, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(o, formatting, JsonSerializerSettings);
        }

        public static T Deserialize<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s, JsonSerializerSettings);
        }
    }
}
