// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentDtoConverter.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

internal class DiscordMessageComponentDtoConverter : JsonConverter<DiscordMessageComponentDto>
{
	public override DiscordMessageComponentDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var json = reader.ReadObjectAsJson();

		var component = JsonSerializer.Deserialize<Component>(json: json, options: options);
		if (component == null)
		{
			throw new InvalidOperationException(message: "Component does not contain type.");
		}

		var type = (DiscordMessageComponentType)component.Type;
		switch (type)
		{
			case DiscordMessageComponentType.Button:
				var buttonDto = JsonSerializer.Deserialize<DiscordMessageButtonDto>(json: json, options: options);
				if (buttonDto == null)
				{
					throw new JsonException(message: $"Failed to deserialize {nameof(DiscordMessageButtonDto)}.");
				}

				return buttonDto;

			default:
				throw new NotSupportedException(message: $"{nameof(DiscordMessageComponentType)} {type} is not supported.");
		}
	}

	public override void Write(Utf8JsonWriter writer, DiscordMessageComponentDto value, JsonSerializerOptions options)
	{
		// Serialize as object in order to get property values from derived classes
		// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
		var json = JsonSerializer.Serialize<object>(value: value, options: options);
		writer.WriteRawValue(json: json);
	}

	private record Component(
		[property: JsonPropertyName(name: "type")]
		int Type
	);
}