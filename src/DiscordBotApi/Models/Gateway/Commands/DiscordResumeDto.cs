// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResumeDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordResumeDto(
	[property: JsonPropertyName(name: "token")]
	string Token,
	[property: JsonPropertyName(name: "session_id")]
	string SessionId,
	[property: JsonPropertyName(name: "seq")]
	int SequenceNumber
);