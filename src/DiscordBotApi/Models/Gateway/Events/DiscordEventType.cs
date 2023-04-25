// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEventType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#commands-and-events-gateway-events
internal enum DiscordEventType
{
	// Hello - Defined in DiscordGatewayPayloadOpcode
	Ready,
	Resumed,

	// Reconnect - Defined in DiscordGatewayPayloadOpcode
	// InvalidSession - Defined in DiscordGatewayPayloadOpcode
	ApplicationCommandPermissionsUpdate,
	ChannelCreate,
	ChannelUpdate,
	ChannelDelete,
	ChannelPinsUpdate,
	ThreadCreate,
	ThreadUpdate,
	ThreadDelete,
	ThreadListSync,
	ThreadMemberUpdate,
	ThreadMembersUpdate,
	GuildCreate,
	GuildUpdate,
	GuildDelete,
	GuildBanAdd,
	GuildBanRemove,
	GuildEmojisUpdate,
	GuildStickersUpdate,
	GuildIntegrationsUpdate,
	GuildMemberAdd,
	GuildMemberRemove,
	GuildMemberUpdate,
	GuildMembersChunk,
	GuildRoleCreate,
	GuildRoleUpdate,
	GuildRoleDelete,
	GuildScheduledEventCreate,
	GuildScheduledEventUpdate,
	GuildScheduledEventDelete,
	GuildScheduledEventUserAdd,
	GuildScheduledEventUserRemove,
	IntegrationCreate,
	IntegrationUpdate,
	IntegrationDelete,
	InteractionCreate,
	InviteCreate,
	InviteDelete,
	MessageCreate,
	MessageUpdate,
	MessageDelete,
	MessageDeleteBulk,
	MessageReactionAdd,
	MessageReactionRemove,
	MessageReactionRemoveAll,
	MessageReactionRemoveEmoji,
	PresenceUpdate,
	StageInstanceCreate,
	StageInstanceUpdate,
	StageInstanceDelete,
	TypingStart,
	UserUpdate,
	VoiceStateUpdate,
	VoiceServerUpdate,
	WebhooksUpdate
}