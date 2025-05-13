// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDeleteDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#message-delete-message-delete-event-fields
internal sealed record DiscordMessageDeleteDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "channel_id")]
	string ChannelId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId
)
{
	public DiscordMessageDelete ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			ChannelId = ulong.Parse(s: ChannelId),
			GuildId = GuildId is not null ? ulong.Parse(s: GuildId) : null
		};
}