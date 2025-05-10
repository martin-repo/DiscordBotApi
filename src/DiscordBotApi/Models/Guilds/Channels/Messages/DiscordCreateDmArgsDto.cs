// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/user#create-dm-json-params
internal sealed record DiscordCreateDmArgsDto(
	[property: JsonPropertyName(name: "recipient_id")]
	string RecipientId
)
{
	public static DiscordCreateDmArgsDto FromModel(DiscordCreateDmArgs model) =>
		new(RecipientId: model.RecipientId.ToString());
}