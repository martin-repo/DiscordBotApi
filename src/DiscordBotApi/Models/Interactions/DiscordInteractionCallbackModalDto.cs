// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackModalDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-modal
internal sealed class DiscordInteractionCallbackModalDto : DiscordInteractionCallbackDataDto
{
	[JsonPropertyName(name: "components")]
	public required ImmutableArray<DiscordMessageComponentDto> Components { get; init; }

	[JsonPropertyName(name: "custom_id")]
	public required string CustomId { get; init; }

	[JsonPropertyName(name: "title")]
	public required string Title { get; init; }

	public static DiscordInteractionCallbackModalDto FromModel(DiscordInteractionCallbackModal model) =>
		new()
		{
			CustomId = model.CustomId,
			Title = model.Title,
			Components = model.Components.Select(selector: DiscordMessageComponentDto.FromModel).ToImmutableArray()
		};
}