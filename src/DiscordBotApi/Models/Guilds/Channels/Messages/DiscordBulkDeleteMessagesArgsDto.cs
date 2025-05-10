// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

internal sealed record DiscordBulkDeleteMessagesArgsDto(
	[property: JsonPropertyName(name: "messages")]
	string[] Messages
)
{
	public static DiscordBulkDeleteMessagesArgsDto FromModel(DiscordBulkDeleteMessagesArgs model) =>
		new(Messages: model.Messages.Select(selector: m => m.ToString()).ToArray());
}