// -------------------------------------------------------------------------------------------------
// <copyright file="JsonDataConverter.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using DiscordBotApi.Utilities;

    internal class JsonDataConverter : JsonConverter<JsonData>
    {
        public override JsonData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = reader.ReadObjectAsJson();
            return new JsonData(json);
        }

        public override void Write(Utf8JsonWriter writer, JsonData value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.Json);
        }
    }
}