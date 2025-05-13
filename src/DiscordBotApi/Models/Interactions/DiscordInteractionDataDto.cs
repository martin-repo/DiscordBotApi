// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDataDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-application-command-data-structure
internal sealed class DiscordInteractionDataDto
{
	[JsonPropertyName(name: "components")]
	public ImmutableArray<DiscordMessageComponentDto>? Components { get; init; }

	[JsonPropertyName(name: "component_type")]
	public int? ComponentType { get; init; }

	[JsonPropertyName(name: "custom_id")]
	public string? CustomId { get; init; }

	[JsonPropertyName(name: "id")]
	public string? Id { get; init; }

	[JsonPropertyName(name: "name")]
	public string? Name { get; init; }

	[JsonPropertyName(name: "options")]
	public ImmutableArray<DiscordApplicationCommandInteractionDataOptionDto>? Options { get; init; }

	[JsonPropertyName(name: "type")]
	public int? Type { get; init; }

	[JsonPropertyName(name: "values")]
	public ImmutableArray<string>? Values { get; init; }

	public DiscordInteractionData ToModel() =>
		new()
		{
			Id = Id != null ? ulong.Parse(s: Id) : null,
			Name = Name,
			Type = Type != null ? (DiscordApplicationCommandType)Type : null,
			Options = Options?.Select(selector: o => o.ToModel()).ToImmutableArray(),
			CustomId = CustomId,
			ComponentType = ComponentType != null ? (DiscordMessageComponentType)ComponentType : null,
			Values = Values?.ToImmutableArray(),
			Components = Components?.Select(selector: c => c.ToModel()).ToImmutableArray()
		};
}