// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRowDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#action-rows
internal sealed record DiscordMessageActionRowDto(
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[] Components
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.ActionRow)
{
	public static DiscordMessageActionRowDto FromModel(DiscordMessageActionRow model) =>
		new(Components: model.Components.Select(selector: DiscordMessageComponentDto.FromModel).ToArray());

	public override DiscordMessageActionRow ToModel() =>
		new() { Components = Components.Select(selector: component => component.ToModel()).ToArray() };
}