// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackMessageDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Interactions;

internal sealed class DiscordInteractionCallbackMessageDto : DiscordInteractionCallbackDataDto
{
	[JsonPropertyName(name: "attachments")]
	public ImmutableArray<DiscordMessageAttachmentDto>? Attachments { get; init; }

	[JsonPropertyName(name: "components")]
	public ImmutableArray<DiscordMessageComponentDto>? Components { get; init; }

	[JsonPropertyName(name: "content")]
	public string? Content { get; init; }

	[JsonPropertyName(name: "embeds")]
	public ImmutableArray<DiscordEmbedDto>? Embeds { get; init; }

	[JsonPropertyName(name: "flags")]
	public uint? Flags { get; init; }

	public static DiscordInteractionCallbackMessageDto FromModel(DiscordInteractionCallbackMessage model) =>
		new()
		{
			Content = model.Content,
			Embeds = model.Embeds?.Select(selector: DiscordEmbedDto.FromModel).ToImmutableArray(),
			Flags = model.Flags != null ? (uint)model.Flags : null,
			Components = model.Components?.Select(selector: DiscordMessageComponentDto.FromModel).ToImmutableArray(),
			Attachments = model.Attachments?.Select(selector: DiscordMessageAttachmentDto.FromModel).ToImmutableArray()
		};
}