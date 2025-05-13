// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordHelloDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Events;

internal sealed record DiscordHelloDto(
	[property: JsonPropertyName(name: "heartbeat_interval")]
	int HeartbeatInterval
);