using System;
using System.Globalization;
using Newtonsoft.Json;
using NodaTime;

namespace BeYourCoach.Common.Json
{
    public class LocalDateJsonConverter : JsonConverter
    {
        private const string DateFormat = "dd-MM-yyyy";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (LocalDate) value;
            serializer.Serialize(writer, date.ToString(DateFormat, CultureInfo.InvariantCulture));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dateTime = DateTime.ParseExact(serializer.Deserialize<string>(reader), DateFormat, CultureInfo.InvariantCulture);
            return new LocalDate(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LocalDate);
        }
    }
}
