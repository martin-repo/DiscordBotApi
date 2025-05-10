// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButtonDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

internal sealed record DiscordMessageButtonDto(
	[property: JsonPropertyName(name: "style")]
	int Style,
	[property: JsonPropertyName(name: "label")]
	string? Label,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto? Emoji,
	[property: JsonPropertyName(name: "custom_id")]
	string? CustomId,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "disabled")]
	bool? Disabled
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.Button)
{
	public static DiscordMessageButtonDto FromModel(DiscordMessageButton model) =>
		new(
			Style: (int)model.Style,
			Label: model.Label,
			Emoji: model.Emoji is not null ? DiscordEmojiDto.FromModel(model: model.Emoji) : null,
			CustomId: model.CustomId,
			Url: model.Url,
			Disabled: model.Disabled
		);

	public override DiscordMessageButton ToModel() =>
		new()
		{
			Style = (DiscordMessageButtonStyle)Style,
			Label = Label,
			Emoji = Emoji?.ToModel(),
			CustomId = CustomId,
			Url = Url,
			Disabled = Disabled
		};
}