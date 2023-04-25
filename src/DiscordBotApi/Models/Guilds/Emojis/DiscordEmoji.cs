// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmoji.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Emojis;

public record DiscordEmoji()
{
	internal DiscordEmoji(DiscordEmojiDto dto) : this()
	{
		Id = dto.Id != null
			? ulong.Parse(s: dto.Id)
			: null;
		Name = dto.Name;
		RequireColons = dto.RequireColons;
		Animated = dto.Animated;
		Available = dto.Available;
	}

	public bool? Animated { get; init; }

	public bool? Available { get; init; }

	public ulong? Id { get; init; }

	public string? Name { get; init; }

	public bool? RequireColons { get; init; }
}