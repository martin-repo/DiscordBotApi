// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButton.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public record DiscordMessageButton() : DiscordMessageComponent
{
	internal DiscordMessageButton(DiscordMessageButtonDto dto) : this()
	{
		Style = (DiscordMessageButtonStyle)dto.Style;
		Label = dto.Label;
		Emoji = dto.Emoji != null
			? new DiscordEmoji(dto: dto.Emoji)
			: null;
		CustomId = dto.CustomId;
		Url = dto.Url;
		Disabled = dto.Disabled;
	}

	public string? CustomId { get; init; }

	public bool? Disabled { get; init; }

	public DiscordEmoji? Emoji { get; init; }

	public string? Label { get; init; }

	public DiscordMessageButtonStyle Style { get; init; }

	public string? Url { get; init; }
}