﻿// -------------------------------------------------------------------------------------------------
// <copyright file="JsonDataConverter.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Gateway;

internal class JsonDataConverter : JsonConverter<JsonData>
{
	public override JsonData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var json = reader.ReadObjectAsJson();
		return new JsonData(Json: json);
	}

	public override void Write(Utf8JsonWriter writer, JsonData value, JsonSerializerOptions options) =>
		writer.WriteRawValue(json: value.Json);
}