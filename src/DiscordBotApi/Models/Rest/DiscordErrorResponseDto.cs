// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordErrorResponseDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Models.Rest;

internal record DiscordErrorResponseDto(
	[property: JsonPropertyName(name: "code")]
	int Code,
	[property: JsonPropertyName(name: "message")]
	string Message,
	[property: JsonPropertyName(name: "errors")]
	JsonData? JsonKey
);