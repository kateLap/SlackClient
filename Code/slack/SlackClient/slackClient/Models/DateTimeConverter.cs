using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SlackClient.Models
{
    public class EpochDateTimeConverter : DateTimeConverterBase
    {

        private static readonly DateTime _Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTimeOffset _EpochOffset = new DateTimeOffset(_Epoch);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nullable = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);

            var t = nullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new SlackClientException("Cannot convert null value");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.Date)
            {
                if (t == typeof(DateTimeOffset))
                {
                    return (reader.Value is DateTimeOffset) ? reader.Value : new DateTimeOffset((DateTime)reader.Value);
                }

                if (reader.Value is DateTimeOffset)
                {
                    return ((DateTimeOffset)reader.Value).DateTime;
                }

                return reader.Value;
            }

            long millisecs = 0;

            if (reader.TokenType == JsonToken.Integer)
            {
                millisecs = (long)reader.Value * 1000;
            }
            else if (reader.TokenType == JsonToken.Float)
            {
                millisecs = (long)((double)reader.Value * 1000);
            }
            else if (reader.TokenType == JsonToken.String)
            {
                string str = ((string)reader.Value);
               // string str1 = str.Replace(".", ",");
                try
                {
                    millisecs = (long)(double.Parse(str/*str1*/) * 1000);
                }
                catch (Exception e)
                {
                    throw new SlackClientException("Unexpected token parsing date");
                }
            }

            var date = _Epoch.AddMilliseconds(millisecs);
            date = date.AddHours(3);//utc+3
            if (t == typeof(DateTimeOffset))
            {
                return new DateTimeOffset(date);
            }
            else
            {
                return date;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double seconds = 0;

            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                dateTime = dateTime.ToUniversalTime();

                seconds = dateTime.Subtract(_Epoch).TotalSeconds;
            }
            else if (value is DateTimeOffset)
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
                dateTimeOffset = dateTimeOffset.ToUniversalTime();

                seconds = dateTimeOffset.Subtract(_EpochOffset).TotalSeconds;
            }
            else
            {
                throw new JsonSerializationException(string.Format("Unexpected value when converting date"));
            }

            writer.WriteValue(seconds);
        }
    }
}
