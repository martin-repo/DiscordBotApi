// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateThreadWithoutMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#start-thread-without-message
internal sealed record DiscordCreateThreadWithoutMessageArgsDto(
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
	public static DiscordCreateThreadWithoutMessageArgsDto FromModel(DiscordCreateThreadWithoutMessageArgs model) =>
		new(
			Name: model.Name,
			AutoArchiveDuration: model.AutoArchiveDuration,
			Type: (int?)model.Type,
			Invitable: model.Invitable,
			RateLimitPerUser: model.RateLimitPerUser
		);
}