// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRow.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public record DiscordMessageActionRow() : DiscordMessageComponent
{
	internal DiscordMessageActionRow(DiscordMessageActionRowDto dto) : this()
	{
		Components = dto.Components.Select(selector: DiscordMessageComponentDto.ConvertToModel)
			.ToArray();
	}

	public IReadOnlyCollection<DiscordMessageComponent> Components { get; set; } = Array.Empty<DiscordMessageComponent>();
}