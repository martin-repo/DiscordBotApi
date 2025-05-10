// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientEvents.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json;

using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Gateway.Commands;
using DiscordBotApi.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;

namespace DiscordBotApi;

public partial class DiscordBotClient
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

	private void HandleGatewayDispatch(DiscordEventType eventType, string eventDataJson)
	{
		switch (eventType)
		{
			case DiscordEventType.ApplicationCommandPermissionsUpdate:
				InvokeEvent<DiscordGuildApplicationCommandPermissions, DiscordGuildApplicationCommandPermissionsDto>(
					eventHandler: ApplicationCommandPermissionsUpdate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.ChannelCreate:
				InvokeEvent<DiscordChannel, DiscordChannelDto>(
					eventHandler: ChannelCreate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.ChannelUpdate:
				InvokeEvent<DiscordChannel, DiscordChannelDto>(
					eventHandler: ChannelUpdate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.ChannelDelete:
				InvokeEvent<DiscordChannel, DiscordChannelDto>(
					eventHandler: ChannelDelete,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.ChannelPinsUpdate: break;
			case DiscordEventType.ThreadCreate: break;
			case DiscordEventType.ThreadUpdate: break;
			case DiscordEventType.ThreadDelete: break;
			case DiscordEventType.ThreadListSync: break;
			case DiscordEventType.ThreadMemberUpdate: break;
			case DiscordEventType.ThreadMembersUpdate: break;
			case DiscordEventType.GuildCreate:
				InvokeEvent<DiscordGuild, DiscordGuildDto>(
					eventHandler: GuildCreate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildUpdate: break;
			case DiscordEventType.GuildDelete: break;
			case DiscordEventType.GuildBanAdd: break;
			case DiscordEventType.GuildBanRemove: break;
			case DiscordEventType.GuildEmojisUpdate: break;
			case DiscordEventType.GuildStickersUpdate: break;
			case DiscordEventType.GuildIntegrationsUpdate: break;
			case DiscordEventType.GuildMemberAdd:
				InvokeEvent<DiscordGuildMemberAdd, DiscordGuildMemberAddDto>(
					eventHandler: GuildMemberAdd,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildMemberRemove:
				InvokeEvent<DiscordGuildMemberRemove, DiscordGuildMemberRemoveDto>(
					eventHandler: GuildMemberRemove,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildMemberUpdate:
				InvokeEvent<DiscordGuildMemberUpdate, DiscordGuildMemberUpdateDto>(
					eventHandler: GuildMemberUpdate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildMembersChunk: break;
			case DiscordEventType.GuildRoleCreate:
				InvokeEvent<DiscordGuildRoleCreate, DiscordGuildRoleCreateDto>(
					eventHandler: GuildRoleCreate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildRoleUpdate:
				InvokeEvent<DiscordGuildRoleUpdate, DiscordGuildRoleUpdateDto>(
					eventHandler: GuildRoleUpdate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildRoleDelete:
				InvokeEvent<DiscordGuildRoleDelete, DiscordGuildRoleDeleteDto>(
					eventHandler: GuildRoleDelete,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.GuildScheduledEventCreate: break;
			case DiscordEventType.GuildScheduledEventUpdate: break;
			case DiscordEventType.GuildScheduledEventDelete: break;
			case DiscordEventType.GuildScheduledEventUserAdd: break;
			case DiscordEventType.GuildScheduledEventUserRemove: break;
			case DiscordEventType.IntegrationCreate: break;
			case DiscordEventType.IntegrationUpdate: break;
			case DiscordEventType.IntegrationDelete: break;
			case DiscordEventType.InteractionCreate:
				InvokeEvent<DiscordInteraction, DiscordInteractionDto>(
					eventHandler: InteractionCreate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.InviteCreate: break;
			case DiscordEventType.InviteDelete: break;
			case DiscordEventType.MessageCreate:
				InvokeEvent<DiscordMessage, DiscordMessageDto>(
					eventHandler: MessageCreate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.MessageUpdate:
				InvokeEvent<DiscordUpdatedMessage, DiscordUpdatedMessageDto>(
					eventHandler: MessageUpdate,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.MessageDelete:
				InvokeEvent<DiscordMessageDelete, DiscordMessageDeleteDto>(
					eventHandler: MessageDelete,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.MessageDeleteBulk: break;
			case DiscordEventType.MessageReactionAdd:
				InvokeEvent<DiscordMessageReactionAdd, DiscordMessageReactionAddDto>(
					eventHandler: MessageReactionAdd,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.MessageReactionRemove:
				InvokeEvent<DiscordMessageReactionRemove, DiscordMessageReactionRemoveDto>(
					eventHandler: MessageReactionRemove,
					eventDataJson: eventDataJson,
					modelFactory: dto => dto.ToModel()
				);
				break;
			case DiscordEventType.MessageReactionRemoveAll: break;
			case DiscordEventType.MessageReactionRemoveEmoji: break;
			case DiscordEventType.PresenceUpdate: break;
			case DiscordEventType.StageInstanceCreate: break;
			case DiscordEventType.StageInstanceUpdate: break;
			case DiscordEventType.StageInstanceDelete: break;
			case DiscordEventType.TypingStart: break;
			case DiscordEventType.UserUpdate: break;
			case DiscordEventType.VoiceStateUpdate: break;
			case DiscordEventType.VoiceServerUpdate: break;
			case DiscordEventType.WebhooksUpdate: break;
			default: throw new NotSupportedException(message: $"{typeof(DiscordEventType)} {eventType} is not supported");
		}
	}

	private void InvokeEvent<TModel, TDto>(
		EventHandler<TModel>? eventHandler,
		string eventDataJson,
		Func<TDto, TModel> modelFactory
	)
		where TDto : class
	{
		var dto = JsonSerializer.Deserialize<TDto>(json: eventDataJson);
		if (dto == null)
		{
			throw new JsonException(message: $"Failed to deserialize {typeof(TDto).Name}");
		}

		var model = modelFactory(arg: dto);

		eventHandler?.Invoke(sender: this, e: model);
	}
}