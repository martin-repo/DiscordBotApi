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
	public event EventHandler<DiscordGuildApplicationCommandPermissions>? ApplicationCommandPermissionsUpdate;

	public event EventHandler<DiscordChannel>? ChannelCreate;

	public event EventHandler<DiscordChannel>? ChannelDelete;

	public event EventHandler<DiscordChannel>? ChannelUpdate;

	public event EventHandler<DiscordGuild>? GuildCreate;

	public event EventHandler<DiscordGuildMemberAdd>? GuildMemberAdd;

	public event EventHandler<DiscordGuildMemberRemove>? GuildMemberRemove;

	public event EventHandler<DiscordGuildMemberUpdate>? GuildMemberUpdate;

	public event EventHandler<DiscordGuildRoleCreate>? GuildRoleCreate;

	public event EventHandler<DiscordGuildRoleDelete>? GuildRoleDelete;

	public event EventHandler<DiscordGuildRoleUpdate>? GuildRoleUpdate;

	public event EventHandler<DiscordInteraction>? InteractionCreate;

	public event EventHandler<DiscordMessage>? MessageCreate;

	public event EventHandler<DiscordMessageDelete>? MessageDelete;

	public event EventHandler<DiscordMessageReactionAdd>? MessageReactionAdd;

	public event EventHandler<DiscordMessageReactionRemove>? MessageReactionRemove;

	public event EventHandler<DiscordUpdatedMessage>? MessageUpdate;
}