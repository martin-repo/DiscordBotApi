// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooterDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

// https://discord.com/developers/docs/developer-tools/embedded-app-sdk#embedfooter
internal sealed record DiscordFooterDto(
	[property: JsonPropertyName(name: "text")]
	string Text,
	[property: JsonPropertyName(name: "icon_url")]
	string? IconUrl
)
{
	public static DiscordFooterDto FromModel(DiscordFooter model) => new(Text: model.Text, IconUrl: model.IconUrl);

	public DiscordFooter ToModel() =>
		new()
		{
			Text = Text,
			IconUrl = IconUrl
		};
}