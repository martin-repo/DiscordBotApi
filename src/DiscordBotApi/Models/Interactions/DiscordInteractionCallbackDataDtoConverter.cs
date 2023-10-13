// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackDataDtoConverter.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Interactions;

internal class DiscordInteractionCallbackDataDtoConverter : JsonConverter<DiscordInteractionCallbackDataDto>
{
	public override DiscordInteractionCallbackDataDto Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	) =>
		throw new NotSupportedException(message: "Deserialization is not supported.");

	public override void Write(Utf8JsonWriter writer, DiscordInteractionCallbackDataDto value, JsonSerializerOptions options)
	{
		// Serialize as object in order to get property values from derived classes
		// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
		var json = JsonSerializer.Serialize<object>(value: value, options: options);
		writer.WriteRawValue(json: json);
	}
}