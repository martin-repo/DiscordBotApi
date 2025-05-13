// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateForumThreadArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel
internal sealed record DiscordCreateForumThreadArgsDto(
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
	public static DiscordCreateForumThreadArgsDto FromModel(DiscordCreateForumThreadArgs model) =>
		new(
			Name: model.Name,
			AutoArchiveDuration: model.AutoArchiveDuration,
			RateLimitPerUser: model.RateLimitPerUser,
			Message: DiscordForumMessageArgsDto.FromModel(model: model.Message),
			AppliedTags: model.AppliedTags?.Select(selector: t => t.ToString()).ToArray()
		);
}