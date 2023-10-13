// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordField.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

public record DiscordField()
{
	internal DiscordField(DiscordFieldDto dto) : this()
	{
		Name = dto.Name;
		Value = dto.Value;
		Inline = dto.Inline;
	}

	public bool? Inline { get; init; }

	public string Name { get; init; } = "";

	public string Value { get; init; } = "";
}