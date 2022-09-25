using Newtonsoft.Json;
using System.Diagnostics;

namespace InvestorAPI.JsonConverters
{
    /// <summary>
    /// An implementation of <see cref="JsonConverter"/> that's used to provide custom
    /// conversion for <see langword="enum"/> data types.
    /// </summary>
    /// <remarks>
    /// The difference from default conversion is focused on providing better error messages for
    /// the client when conversion fails.
    /// </remarks>
    public class EnumJsonConverter : JsonConverter
    {

        [DebuggerStepThrough]
        public override bool CanConvert(Type objectType)
        {
            // True if the type is enum or nullable enum.
            return objectType.IsEnum || Nullable.GetUnderlyingType(objectType)?.IsEnum == true;
        }


        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            // Check if enum is nullable.
            if (Nullable.GetUnderlyingType(objectType) is Type type)
            {
                if (reader.Value is null)
                {
                    return null;
                }
                // Set to the underlying type.
                objectType = type;
            }

            // Try parse if the value provided is in string or numeral format.
            if (!(reader.Value is string or long or int && Enum.TryParse(objectType, reader.Value.ToString(), out object? result)))
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
            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }

    }

}
