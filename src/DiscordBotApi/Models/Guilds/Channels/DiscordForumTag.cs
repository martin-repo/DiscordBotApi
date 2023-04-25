// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumTag.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#forum-tag-object
public record DiscordForumTag()
{
	internal DiscordForumTag(DiscordForumTagDto dto) : this()
	{
		Id = dto.Id != null
			? ulong.Parse(s: dto.Id)
			: null;
		Name = dto.Name;
		Moderated = dto.Moderated;
		EmojiId = dto.EmojiId != null
			? ulong.Parse(s: dto.EmojiId)
			: null;
		EmojiName = dto.EmojiName;
	}

	public ulong? EmojiId { get; init; }

	public string? EmojiName { get; init; }

	public ulong? Id { get; init; }

	public bool? Moderated { get; init; }

	public string Name { get; init; } = string.Empty;
}