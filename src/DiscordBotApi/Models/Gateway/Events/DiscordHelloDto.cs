// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordHelloDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Events;

internal record DiscordHelloDto(
	[property: JsonPropertyName(name: "heartbeat_interval")]
	int HeartbeatInterval
);