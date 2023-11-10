// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateThreadWithoutMessageArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#start-thread-without-message
internal record DiscordCreateThreadWithoutMessageArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "auto_archive_duration")]
	int? AutoArchiveDuration,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "invitable")]
	bool? Invitable,
	[property: JsonPropertyName(name: "rate_limit_per_user")]
	int? RateLimitPerUser
)
{
	internal DiscordCreateThreadWithoutMessageArgsDto(DiscordCreateThreadWithoutMessageArgs model) : this(
		Name: model.Name,
		AutoArchiveDuration: model.AutoArchiveDuration,
		Type: (int?)model.Type,
		Invitable: model.Invitable,
		RateLimitPerUser: model.RateLimitPerUser)
	{
	}
}