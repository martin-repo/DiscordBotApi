// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAddDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-member-add
internal record DiscordGuildMemberAddDto(
	DiscordUserDto? User,
	string? Nick,
	string[] Roles,
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId
) : DiscordGuildMemberDto(User: User, Nick: Nick, Roles: Roles);