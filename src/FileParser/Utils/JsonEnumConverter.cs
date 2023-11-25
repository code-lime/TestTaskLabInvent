using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileParser.Utils;

public class JsonEnumConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsEnum;
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => (JsonConverter?)Activator.CreateInstance(typeof(Converter<>).MakeGenericType(typeToConvert));

    private class Converter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.GetString() is string input
            ? input.TryParseEnumMember(out T value)
                ? value
                : throw new FormatException(input)
            : throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.GetEnumMember());
    }
}
