// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionData.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

public record DiscordInteractionData
{
	internal DiscordInteractionData(DiscordInteractionDataDto dto)
	{
		Id = dto.Id != null
			? ulong.Parse(s: dto.Id)
			: null;
		Name = dto.Name;
		Type = dto.Type != null
			? (DiscordApplicationCommandType)dto.Type
			: null;
		Options = dto.Options?.Select(selector: o => new DiscordApplicationCommandInteractionDataOption(dto: o))
			.ToArray();
		CustomId = dto.CustomId;
		ComponentType = dto.ComponentType != null
			? (DiscordMessageComponentType)dto.ComponentType
			: null;
	}

	public DiscordMessageComponentType? ComponentType { get; init; }

	public string? CustomId { get; init; }

	public ulong? Id { get; init; }

	public string? Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

	public DiscordApplicationCommandType? Type { get; init; }
}