// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumTag.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#forum-tag-object
public sealed class DiscordForumTag
{
	public ulong? EmojiId { get; init; }

	public string? EmojiName { get; init; }

	public ulong? Id { get; init; }

	public bool? Moderated { get; init; }

	public required string Name { get; init; }
}