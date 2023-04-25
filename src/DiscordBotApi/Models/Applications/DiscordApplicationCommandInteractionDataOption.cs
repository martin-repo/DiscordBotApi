// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOption.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

public record DiscordApplicationCommandInteractionDataOption
{
	internal DiscordApplicationCommandInteractionDataOption(DiscordApplicationCommandInteractionDataOptionDto dto)
	{
		Name = dto.Name;
		Type = (DiscordApplicationCommandOptionType)dto.Type;
		Value = JsonParseUtils.ToObject(type: (DiscordApplicationCommandOptionType)dto.Type, jsonValue: dto.Value);
		Options = dto.Options?.Select(selector: o => new DiscordApplicationCommandInteractionDataOption(dto: o))
			.ToArray();
		Focused = dto.Focused;
	}

	public bool? Focused { get; init; }

	public string Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

	public DiscordApplicationCommandOptionType Type { get; init; }

	public object Value { get; init; }
}