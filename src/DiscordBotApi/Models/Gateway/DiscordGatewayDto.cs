// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

internal record DiscordGatewayDto(
	[property: JsonPropertyName(name: "url")]
	string Url
);