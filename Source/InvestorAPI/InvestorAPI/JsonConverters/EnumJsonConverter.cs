using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvestorAPI.JsonConverters
{
    /// <summary>
    /// An extension of <see cref="StringEnumConverter"/> that's used to provide custom
    /// exception handling with a better error message.
    /// </summary>
    public class EnumJsonConverter : StringEnumConverter
    {

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                string valueDescriptor = reader.TokenType switch
                {
                    JsonToken.Null => "null value",
                    JsonToken.StartArray => "array object",
                    JsonToken.StartObject => "object body",
                    _ => $"value '{reader.Value?.ToString()}'"
                };
                throw new JsonSerializationException($"Error converting {valueDescriptor} to type '{objectType.Name}'.");
            }
        }

    }

}
