// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdateDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Utilities;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/events/gateway-events#update-presence-gateway-presence-update-structure
internal sealed class DiscordPresenceUpdateDto
{
	[JsonPropertyName(name: "activities")]
	public required ImmutableArray<DiscordActivityUpdateDto> Activities { get; init; }

	[JsonPropertyName(name: "afk")]
	public required bool Afk { get; init; }

	[JsonPropertyName(name: "since")]
	public int? Since { get; init; }

	[JsonPropertyName(name: "status")]
	public required string Status { get; init; }

	public static DiscordPresenceUpdateDto FromModel(DiscordPresenceUpdate model) =>
		new()
		{
			Since = model.Since != null ? DateTimeUtils.ToEpochTimeMilliseconds(datetime: model.Since.Value) : null,
			Activities = model.Activities.Select(selector: DiscordActivityUpdateDto.FromModel).ToImmutableArray(),
			Status = GetStatusString(status: model.Status),
			Afk = model.Afk
		};

	private static string GetStatusString(DiscordPresenceStatus status) =>
		status switch
		{
			DiscordPresenceStatus.Online => "online",
			DiscordPresenceStatus.DoNotDisturb => "dnd",
			DiscordPresenceStatus.Idle => "idle",
			DiscordPresenceStatus.Invisible => "invisible",
			DiscordPresenceStatus.Offline => "offline",
			_ => throw new NotSupportedException(message: $"{nameof(DiscordPresenceStatus)} {status} is not supported")
		};
}