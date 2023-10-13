// -------------------------------------------------------------------------------------------------
// <copyright file="Utf8JsonReaderExtensions.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text;
using System.Text.Json;

namespace DiscordBotApi.Utilities;

internal static class Utf8JsonReaderExtensions
{
	public static string ReadObjectAsJson(ref this Utf8JsonReader reader)
	{
		var jsonBuilder = new StringBuilder();

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			return ParseSingleValue(reader: ref reader);
		}

		jsonBuilder.Append(value: '{');
		reader.Read();

		var startDepth = reader.CurrentDepth;
		var lastTokenType = JsonTokenType.None;

		var depthItems = new Stack<Depth>();
		depthItems.Push(item: new Depth());

		while (reader.CurrentDepth >= startDepth)
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.StartObject:
					switch (lastTokenType)
					{
						case JsonTokenType.EndObject when depthItems.Peek()
							.IsArray:
							jsonBuilder.Append(value: ',');
							break;
					}

					jsonBuilder.Append(value: '{');
					depthItems.Push(item: new Depth());
					break;
				case JsonTokenType.EndObject:
					jsonBuilder.Append(value: '}');
					depthItems.Pop();
					break;
				case JsonTokenType.StartArray:
					jsonBuilder.Append(value: '[');
					depthItems.Push(item: new Depth { IsArray = true });
					break;
				case JsonTokenType.EndArray:
					jsonBuilder.Append(value: ']');
					depthItems.Pop();
					break;

				case JsonTokenType.PropertyName:
					switch (lastTokenType)
					{
						case JsonTokenType.EndObject:
						case JsonTokenType.EndArray:
						case JsonTokenType.String:
						case JsonTokenType.Number:
						case JsonTokenType.True:
						case JsonTokenType.False:
						case JsonTokenType.Null:
							jsonBuilder.Append(value: ',');
							break;
					}

					jsonBuilder.Append(handler: $@"""{reader.GetString()!}"":");
					break;

				case JsonTokenType.String:
					var depthItem = depthItems.Peek();
					if (depthItem.IsArray &&
						depthItem.IsValueAdded)
					{
						jsonBuilder.Append(value: ',');
					}

					jsonBuilder.Append(value: ParseSingleValue(reader: ref reader));
					depthItem.IsValueAdded = true;
					break;
				case JsonTokenType.Number:
				case JsonTokenType.True:
				case JsonTokenType.False:
				case JsonTokenType.Null:
					jsonBuilder.Append(value: ParseSingleValue(reader: ref reader));
					break;
			}

			lastTokenType = reader.TokenType;
			reader.Read();
		}

		if (reader.TokenType != JsonTokenType.EndObject)
		{
			throw new JsonException(message: "Not at end of object.");
		}

		// Leave reader on the EndObject at the same depth, do not advance further
		jsonBuilder.Append(value: '}');

		var json = jsonBuilder.ToString();
		if (json == null)
		{
			throw new JsonException(message: "Failed to read JSON from object.");
		}

		return json;
	}

	private static string ParseSingleValue(ref Utf8JsonReader reader)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.String:
				var stringValue = reader.GetString()!;
				var encodedString = JsonSerializer.Serialize(value: stringValue);
				return encodedString;
			case JsonTokenType.Number:
				return reader.GetInt64()
					.ToString(provider: CultureInfo.InvariantCulture);
			case JsonTokenType.True:
				return "true";
			case JsonTokenType.False:
				return "false";
			case JsonTokenType.Null:
				return "null";
			default:
				return "";
		}
	}

	private class Depth
	{
		public bool IsArray { get; init; }

		public bool IsValueAdded { get; set; }
	}
}