using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bento.Extensions;

/// <summary>
/// JSON converter that converts IEnumerable&lt;string&gt; to comma-separated string during serialization
/// and comma-separated string to IEnumerable&lt;string&gt; during deserialization.
/// Used for Bento API fields that expect comma-separated tag lists.
/// </summary>
public class StringArrayToCommaSeparatedConverter : JsonConverter<IEnumerable<string>?>
{
    public override IEnumerable<string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return new List<string>();
            }

            return value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(tag => tag.Trim())
                       .Where(tag => !string.IsNullOrWhiteSpace(tag))
                       .ToList();
        }

        throw new JsonException($"Unexpected token type: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, IEnumerable<string>? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        var commaSeparated = string.Join(",", value.Where(tag => !string.IsNullOrWhiteSpace(tag)));
        writer.WriteStringValue(commaSeparated);
    }
}
