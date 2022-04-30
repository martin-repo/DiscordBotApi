// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentDtoConverter.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using DiscordBotApi.Utilities;

    internal class DiscordMessageComponentDtoConverter : JsonConverter<DiscordMessageComponentDto>
    {
        public override DiscordMessageComponentDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = reader.ReadObjectAsJson();

            var component = JsonSerializer.Deserialize<Component>(json, options);
            if (component == null)
            {
                throw new InvalidOperationException("Component does not contain type.");
            }

            var type = (DiscordMessageComponentType)component.Type;
            switch (type)
            {
                case DiscordMessageComponentType.Button:
                    var buttonDto = JsonSerializer.Deserialize<DiscordMessageButtonDto>(json, options);
                    if (buttonDto == null)
                    {
                        throw new JsonException($"Failed to deserialize {nameof(DiscordMessageButtonDto)}.");
                    }

                    return buttonDto;

                default:
                    throw new NotSupportedException($"{nameof(DiscordMessageComponentType)} {type} is not supported.");
            }
        }

        public override void Write(Utf8JsonWriter writer, DiscordMessageComponentDto value, JsonSerializerOptions options)
        {
            // Serialize as object in order to get property values from derived classes
            // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
            var json = JsonSerializer.Serialize<object>(value, options);
            writer.WriteRawValue(json);
        }

        private record Component([property: JsonPropertyName("type")] int Type);
    }
}