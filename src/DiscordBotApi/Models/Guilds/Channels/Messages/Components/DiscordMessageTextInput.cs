// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageTextInput.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public record DiscordMessageTextInput() : DiscordMessageComponent
{
	internal DiscordMessageTextInput(DiscordMessageTextInputDto dto) : this()
	{
		CustomId = dto.CustomId;
		Style = (DiscordMessageTextInputStyle)dto.Style;
		Label = dto.Label;
		MinLength = dto.MinLength;
		MaxLength = dto.MaxLength;
		Required = dto.Required;
		Value = dto.Value;
		Placeholder = dto.Placeholder;
	}

	public string CustomId { get; init; } = null!;

	public string Label { get; init; } = null!;

	public int? MaxLength { get; init; }

	public int? MinLength { get; init; }

	public string? Placeholder { get; init; }

	public bool? Required { get; init; }

	public DiscordMessageTextInputStyle Style { get; init; }

	public string? Value { get; init; }
}