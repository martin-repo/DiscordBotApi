// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResumeDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal sealed record DiscordResumeDto(
	[property: JsonPropertyName(name: "token")]
	string Token,
	[property: JsonPropertyName(name: "session_id")]
	string SessionId,
	[property: JsonPropertyName(name: "seq")]
	int SequenceNumber
);