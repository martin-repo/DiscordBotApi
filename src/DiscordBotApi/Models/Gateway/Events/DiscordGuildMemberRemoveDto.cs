// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberRemoveDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-member-remove-guild-member-remove-event-fields
internal sealed record DiscordGuildMemberRemoveDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User
)
{
	public DiscordGuildMemberRemove ToModel() =>
		new()
		{
			GuildId = ulong.Parse(s: GuildId),
			User = User.ToModel()
		};
}