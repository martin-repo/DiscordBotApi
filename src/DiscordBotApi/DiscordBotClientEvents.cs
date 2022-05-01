// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientEvents.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using System.Text.Json;

    using DiscordBotApi.Models.Gateway.Commands;
    using DiscordBotApi.Models.Gateway.Events;
    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Interactions;

    public partial class DiscordBotClient
    {
        public event EventHandler<DiscordGuildApplicationCommandPermissions>? ApplicationCommandPermissionsUpdate;

        public event EventHandler<DiscordChannel>? ChannelCreate;

        public event EventHandler<DiscordChannel>? ChannelDelete;

        public event EventHandler<DiscordChannel>? ChannelUpdate;

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

        public event EventHandler<DiscordMessage>? MessageUpdate;

        private void HandleGatewayDispatch(DiscordEventType eventType, string eventDataJson)
        {
            switch (eventType)
            {
                case DiscordEventType.ApplicationCommandPermissionsUpdate:
                    InvokeEvent<DiscordGuildApplicationCommandPermissions, DiscordGuildApplicationCommandPermissionsDto>(
                        ApplicationCommandPermissionsUpdate,
                        eventDataJson,
                        dto => new(dto));
                    break;
                case DiscordEventType.ChannelCreate:
                    InvokeEvent<DiscordChannel, DiscordChannelDto>(ChannelCreate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.ChannelUpdate:
                    InvokeEvent<DiscordChannel, DiscordChannelDto>(ChannelUpdate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.ChannelDelete:
                    InvokeEvent<DiscordChannel, DiscordChannelDto>(ChannelDelete, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.ChannelPinsUpdate:
                    break;
                case DiscordEventType.ThreadCreate:
                    break;
                case DiscordEventType.ThreadUpdate:
                    break;
                case DiscordEventType.ThreadDelete:
                    break;
                case DiscordEventType.ThreadListSync:
                    break;
                case DiscordEventType.ThreadMemberUpdate:
                    break;
                case DiscordEventType.ThreadMembersUpdate:
                    break;
                case DiscordEventType.GuildCreate:
                    break;
                case DiscordEventType.GuildUpdate:
                    break;
                case DiscordEventType.GuildDelete:
                    break;
                case DiscordEventType.GuildBanAdd:
                    break;
                case DiscordEventType.GuildBanRemove:
                    break;
                case DiscordEventType.GuildEmojisUpdate:
                    break;
                case DiscordEventType.GuildStickersUpdate:
                    break;
                case DiscordEventType.GuildIntegrationsUpdate:
                    break;
                case DiscordEventType.GuildMemberAdd:
                    InvokeEvent<DiscordGuildMemberAdd, DiscordGuildMemberAddDto>(GuildMemberAdd, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.GuildMemberRemove:
                    InvokeEvent<DiscordGuildMemberRemove, DiscordGuildMemberRemoveDto>(GuildMemberRemove, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.GuildMemberUpdate:
                    InvokeEvent<DiscordGuildMemberUpdate, DiscordGuildMemberUpdateDto>(GuildMemberUpdate, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.GuildMembersChunk:
                    break;
                case DiscordEventType.GuildRoleCreate:
                    InvokeEvent<DiscordGuildRoleCreate, DiscordGuildRoleCreateDto>(GuildRoleCreate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.GuildRoleUpdate:
                    InvokeEvent<DiscordGuildRoleUpdate, DiscordGuildRoleUpdateDto>(GuildRoleUpdate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.GuildRoleDelete:
                    InvokeEvent<DiscordGuildRoleDelete, DiscordGuildRoleDeleteDto>(GuildRoleDelete, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.GuildScheduledEventCreate:
                    break;
                case DiscordEventType.GuildScheduledEventUpdate:
                    break;
                case DiscordEventType.GuildScheduledEventDelete:
                    break;
                case DiscordEventType.GuildScheduledEventUserAdd:
                    break;
                case DiscordEventType.GuildScheduledEventUserRemove:
                    break;
                case DiscordEventType.IntegrationCreate:
                    break;
                case DiscordEventType.IntegrationUpdate:
                    break;
                case DiscordEventType.IntegrationDelete:
                    break;
                case DiscordEventType.InteractionCreate:
                    InvokeEvent<DiscordInteraction, DiscordInteractionDto>(InteractionCreate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.InviteCreate:
                    break;
                case DiscordEventType.InviteDelete:
                    break;
                case DiscordEventType.MessageCreate:
                    InvokeEvent<DiscordMessage, DiscordMessageDto>(MessageCreate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.MessageUpdate:
                    InvokeEvent<DiscordMessage, DiscordMessageDto>(MessageUpdate, eventDataJson, dto => new(this, dto));
                    break;
                case DiscordEventType.MessageDelete:
                    InvokeEvent<DiscordMessageDelete, DiscordMessageDeleteDto>(MessageDelete, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.MessageDeleteBulk:
                    break;
                case DiscordEventType.MessageReactionAdd:
                    InvokeEvent<DiscordMessageReactionAdd, DiscordMessageReactionAddDto>(MessageReactionAdd, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.MessageReactionRemove:
                    InvokeEvent<DiscordMessageReactionRemove, DiscordMessageReactionRemoveDto>(MessageReactionRemove, eventDataJson, dto => new(dto));
                    break;
                case DiscordEventType.MessageReactionRemoveAll:
                    break;
                case DiscordEventType.MessageReactionRemoveEmoji:
                    break;
                case DiscordEventType.PresenceUpdate:
                    break;
                case DiscordEventType.StageInstanceCreate:
                    break;
                case DiscordEventType.StageInstanceUpdate:
                    break;
                case DiscordEventType.StageInstanceDelete:
                    break;
                case DiscordEventType.TypingStart:
                    break;
                case DiscordEventType.UserUpdate:
                    break;
                case DiscordEventType.VoiceStateUpdate:
                    break;
                case DiscordEventType.VoiceServerUpdate:
                    break;
                case DiscordEventType.WebhooksUpdate:
                    break;
                default:
                    throw new NotSupportedException($"{typeof(DiscordEventType)} {eventType} is not supported");
            }
        }

        private void InvokeEvent<TModel, TDto>(EventHandler<TModel>? eventHandler, string eventDataJson, Func<TDto, TModel> modelFactory)
            where TDto : class
        {
            var dto = JsonSerializer.Deserialize<TDto>(eventDataJson);
            if (dto == null)
            {
                throw new JsonException($"Failed to deserialize {typeof(TDto).Name}");
            }

            var model = modelFactory(dto);

            eventHandler?.Invoke(this, model);
        }
    }
}