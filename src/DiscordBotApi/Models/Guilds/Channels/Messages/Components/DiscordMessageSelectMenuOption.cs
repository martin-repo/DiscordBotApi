// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuOption.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public record DiscordMessageSelectMenuOption()
{
	internal DiscordMessageSelectMenuOption(DiscordMessageSelectMenuOptionDto dto) : this()
	{
		Label = dto.Label;
		Value = dto.Value;
		Description = dto.Description;
		Emoji = dto.Emoji != null
			? new DiscordEmoji(dto: dto.Emoji)
			: null;
		Default = dto.Default;
	}

	public bool? Default { get; init; }

	public string? Description { get; init; }

	public DiscordEmoji? Emoji { get; init; }

	public string Label { get; init; } = null!;

	public string Value { get; init; } = null!;
}