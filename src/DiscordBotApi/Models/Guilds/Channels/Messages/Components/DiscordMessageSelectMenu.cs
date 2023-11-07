// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenu.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public record DiscordMessageSelectMenu() : DiscordMessageComponent
{
	internal DiscordMessageSelectMenu(DiscordMessageSelectMenuDto dto) : this()
	{
		CustomId = dto.CustomId;
		Options = dto.Options?.Select(selector: o => new DiscordMessageSelectMenuOption(dto: o))
			.ToImmutableArray();
		Placeholder = dto.Placeholder;
		MinValues = dto.MinValues;
		MaxValues = dto.MaxValues;
		Disabled = dto.Disabled;
	}

	public string CustomId { get; init; } = null!;

	public bool? Disabled { get; init; }

	public int? MaxValues { get; init; }

	public int? MinValues { get; init; }

	public IReadOnlyCollection<DiscordMessageSelectMenuOption>? Options { get; init; }

	public string? Placeholder { get; init; }
}