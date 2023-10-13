// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateForumThreadArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel
internal record DiscordCreateForumThreadArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "auto_archive_duration")]
	int? AutoArchiveDuration,
	[property: JsonPropertyName(name: "rate_limit_per_user")]
	int? RateLimitPerUser,
	[property: JsonPropertyName(name: "message")]
	DiscordForumMessageArgsDto Message,
	[property: JsonPropertyName(name: "applied_tags")]
	string[]? AppliedTags
)
{
	internal DiscordCreateForumThreadArgsDto(DiscordCreateForumThreadArgs model) : this(
		Name: model.Name,
		AutoArchiveDuration: model.AutoArchiveDuration,
		RateLimitPerUser: model.RateLimitPerUser,
		Message: new DiscordForumMessageArgsDto(model: model.Message),
		AppliedTags: model.AppliedTags?.Select(selector: t => t.ToString())
			.ToArray())
	{
	}
}