// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Events.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	event EventHandler<DiscordGuildApplicationCommandPermissions>? ApplicationCommandPermissionsUpdate;

	event EventHandler<DiscordChannel>? ChannelCreate;

	event EventHandler<DiscordChannel>? ChannelDelete;

	event EventHandler<DiscordChannel>? ChannelUpdate;

	event EventHandler<DiscordGuild>? GuildCreate;

	event EventHandler<DiscordGuildMemberAdd>? GuildMemberAdd;

	event EventHandler<DiscordGuildMemberRemove>? GuildMemberRemove;

	event EventHandler<DiscordGuildMemberUpdate>? GuildMemberUpdate;

	event EventHandler<DiscordGuildRoleCreate>? GuildRoleCreate;

	event EventHandler<DiscordGuildRoleDelete>? GuildRoleDelete;

	event EventHandler<DiscordGuildRoleUpdate>? GuildRoleUpdate;

	event EventHandler<DiscordInteraction>? InteractionCreate;

	event EventHandler<DiscordMessage>? MessageCreate;

	event EventHandler<DiscordMessageDelete>? MessageDelete;

	event EventHandler<DiscordMessageReactionAdd>? MessageReactionAdd;

	event EventHandler<DiscordMessageReactionRemove>? MessageReactionRemove;

	event EventHandler<DiscordUpdatedMessage>? MessageUpdate;
}