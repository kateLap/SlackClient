using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SlackClient.Models
{
    /// <summary>
    /// The class converts the time from DateTime format to JSON or on the contrary
    /// </summary>
    public class EpochDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// The initial DateTime value
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// The initial DateTimeOffset value
        /// </summary>
        private static readonly DateTimeOffset EpochOffset = new DateTimeOffset(Epoch);

        /// <summary>
        /// Reads the JSON representation of the object and deserializes it.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="T:SlackClient.Models.SlackClientException">
        /// Cannot convert null value
        /// </exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nullable = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);

            var t = nullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            switch (reader.TokenType)
            {
                case JsonToken.Null when !nullable:
                    throw new SlackClientException("Cannot convert null value");
                case JsonToken.Null:
                    return null;
                case JsonToken.Date when t == typeof(DateTimeOffset):
                    return (reader.Value is DateTimeOffset) ? reader.Value : new DateTimeOffset((DateTime)reader.Value);
                case JsonToken.Date when reader.Value is DateTimeOffset offset:
                    return offset.DateTime;
                case JsonToken.Date:
                    return reader.Value;
            }

            long millis = 0;

            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    millis = (long)reader.Value * 1000;
                    break;
                case JsonToken.Float:
                    millis = (long)((double)reader.Value * 1000);
                    break;
                case JsonToken.String:
                {
                    var str = (string)reader.Value;
                    try
                    {
                        millis = (long)(double.Parse(str) * 1000);
                    }
                    catch (Exception e)
                    {
                        throw new SlackClientException("Unexpected token parsing date", e);
                    }

                    break;
                }
            }

            var date = Epoch.AddMilliseconds(millis);

            date = date.AddHours(3);

            return t == typeof(DateTimeOffset) ? new DateTimeOffset(date) : date;
        }

        /// <summary>
        /// Serializes and writes the JSON representation of the object/
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="T:Newtonsoft.Json.JsonSerializationException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double seconds = 0;

            switch (value)
            {
                case DateTime dateTime:
                    dateTime = dateTime.ToUniversalTime();

                    seconds = dateTime.Subtract(Epoch).TotalSeconds;
                    break;
                case DateTimeOffset dateTimeOffset:
                    dateTimeOffset = dateTimeOffset.ToUniversalTime();

                    seconds = dateTimeOffset.Subtract(EpochOffset).TotalSeconds;
                    break;
                default:
                    throw new JsonSerializationException(string.Format("Unexpected value when converting date"));
            }

            writer.WriteValue(seconds);
        }
    }
}
