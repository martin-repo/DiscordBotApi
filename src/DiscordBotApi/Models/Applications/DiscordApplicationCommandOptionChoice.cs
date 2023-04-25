// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoice.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

public record DiscordApplicationCommandOptionChoice()
{
	internal DiscordApplicationCommandOptionChoice(
		DiscordApplicationCommandOptionType type,
		DiscordApplicationCommandOptionChoiceDto dto
	) : this()
	{
		Name = dto.Name;
		Value = JsonParseUtils.ToObject(type: type, jsonValue: dto.Value);
	}

	public string Name { get; init; } = "";

	public object Value { get; init; } = "";
}