// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdateDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordPresenceUpdateDto(
	[property: JsonPropertyName(name: "since")]
	int? Since,
	[property: JsonPropertyName(name: "activities")]
	DiscordActivityUpdateDto[] Activities,
	[property: JsonPropertyName(name: "status")]
	string Status,
	[property: JsonPropertyName(name: "afk")]
	bool Afk
)
{
	internal DiscordPresenceUpdateDto(DiscordPresenceUpdate model) : this(
		Since: model.Since != null
			? DateTimeUtils.ToEpochTimeMilliseconds(datetime: model.Since.Value)
			: null,
		Activities: model.Activities.Select(selector: a => new DiscordActivityUpdateDto(model: a))
			.ToArray(),
		Status: GetStatusString(status: model.Status),
		Afk: model.Afk)
	{
	}

	private static string GetStatusString(DiscordPresenceStatus status)
	{
		switch (status)
		{
			case DiscordPresenceStatus.Online:
				return "online";
			case DiscordPresenceStatus.DoNotDisturb:
				return "dnd";
			case DiscordPresenceStatus.Idle:
				return "idle";
			case DiscordPresenceStatus.Invisible:
				return "invisible";
			case DiscordPresenceStatus.Offline:
				return "offline";
			default:
				throw new NotSupportedException(message: $"{nameof(DiscordPresenceStatus)} {status} is not supported");
		}
	}
}