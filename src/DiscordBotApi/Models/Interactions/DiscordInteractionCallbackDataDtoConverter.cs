// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackDataDtoConverter.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class DiscordInteractionCallbackDataDtoConverter : JsonConverter<DiscordInteractionCallbackDataDto>
    {
        public override DiscordInteractionCallbackDataDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Deserialization is not supported.");
        }

        public override void Write(Utf8JsonWriter writer, DiscordInteractionCallbackDataDto value, JsonSerializerOptions options)
        {
            // Serialize as object in order to get property values from derived classes
            // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
            var json = JsonSerializer.Serialize<object>(value, options);
            writer.WriteRawValue(json);
        }
    }
}