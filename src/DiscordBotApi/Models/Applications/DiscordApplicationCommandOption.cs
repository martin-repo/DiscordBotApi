// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOption.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

public record DiscordApplicationCommandOption()
{
	internal DiscordApplicationCommandOption(DiscordApplicationCommandOptionDto dto) : this()
	{
		var type = (DiscordApplicationCommandOptionType)dto.Type;
		Type = type;
		Name = dto.Name;
		Description = dto.Description;
		Required = dto.Required;
		Choices = dto.Choices?.Select(selector: c => new DiscordApplicationCommandOptionChoice(type: type, dto: c))
			.ToArray();
		Options = dto.Options?.Select(selector: o => new DiscordApplicationCommandOption(dto: o))
			.ToArray();
		ChannelTypes = dto.ChannelTypes?.Select(selector: t => (DiscordChannelType)t)
			.ToArray();
		MinValue = dto.MinValue != null
			? JsonParseUtils.ToObject(type: type, jsonValue: dto.MinValue)
			: null;
		MaxValue = dto.MaxValue != null
			? JsonParseUtils.ToObject(type: type, jsonValue: dto.MaxValue)
			: null;
		Autocomplete = dto.Autocomplete;
	}

	public bool? Autocomplete { get; init; }

	public IReadOnlyCollection<DiscordChannelType>? ChannelTypes { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandOptionChoice>? Choices { get; init; }

	public string Description { get; init; } = "";

	public object? MaxValue { get; init; }

	public object? MinValue { get; init; }

	public string Name { get; init; } = "";

	public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

	public bool? Required { get; init; }

	public DiscordApplicationCommandOptionType Type { get; init; }
}