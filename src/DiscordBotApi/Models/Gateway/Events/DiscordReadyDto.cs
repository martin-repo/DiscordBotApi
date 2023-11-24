// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReadyDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#ready-ready-event-fields
internal record DiscordReadyDto(
	[property: JsonPropertyName(name: "v")]
	int V,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User,
	[property: JsonPropertyName(name: "guilds")]
	UnavailableGuildDto[] Guilds,
	[property: JsonPropertyName(name: "session_id")]
	string SessionId,
	[property: JsonPropertyName(name: "resume_gateway_url")]
	string ResumeGatewayUrl,
	[property: JsonPropertyName(name: "shard")]
	int[]? Shard,
	[property: JsonPropertyName(name: "application")]
	DiscordApplicationDto Application
);