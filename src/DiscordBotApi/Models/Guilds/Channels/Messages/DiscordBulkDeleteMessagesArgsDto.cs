// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

internal record DiscordBulkDeleteMessagesArgsDto(
	[property: JsonPropertyName(name: "messages")]
	string[] Messages
)
{
	internal DiscordBulkDeleteMessagesArgsDto(DiscordBulkDeleteMessagesArgs model) : this(
		Messages: model.Messages.Select(selector: m => m.ToString())
			.ToArray())
	{
	}
}