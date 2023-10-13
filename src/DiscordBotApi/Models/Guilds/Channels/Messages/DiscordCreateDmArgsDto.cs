// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/user#create-dm-json-params
internal record DiscordCreateDmArgsDto(
	[property: JsonPropertyName(name: "recipient_id")]
	string ReceipientId
)
{
	internal DiscordCreateDmArgsDto(DiscordCreateDmArgs model) : this(ReceipientId: model.RecipientId.ToString())
	{
	}
}