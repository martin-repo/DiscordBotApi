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
                    var applicationCommandPermissionsUpdateDto =
                        JsonSerializer.Deserialize<DiscordGuildApplicationCommandPermissionsDto>(eventDataJson);
                    ApplicationCommandPermissionsUpdate?.Invoke(
                        this,
                        new DiscordGuildApplicationCommandPermissions(applicationCommandPermissionsUpdateDto!));
                    break;
                case DiscordEventType.ChannelCreate:
                    var channelCreateDto = JsonSerializer.Deserialize<DiscordChannelDto>(eventDataJson);
                    ChannelCreate?.Invoke(this, new DiscordChannel(this, channelCreateDto!));
                    break;
                case DiscordEventType.ChannelUpdate:
                    var channelUpdateDto = JsonSerializer.Deserialize<DiscordChannelDto>(eventDataJson);
                    ChannelUpdate?.Invoke(this, new DiscordChannel(this, channelUpdateDto!));
                    break;
                case DiscordEventType.ChannelDelete:
                    var channelDeleteDto = JsonSerializer.Deserialize<DiscordChannelDto>(eventDataJson);
                    ChannelDelete?.Invoke(this, new DiscordChannel(this, channelDeleteDto!));
                    break;
                case DiscordEventType.ChannelPinsUpdate:
                    break;
                case DiscordEventType.GuildCreate:
                    break;
                case DiscordEventType.GuildUpdate:
                    break;
                case DiscordEventType.GuildDelete:
                    break;
                case DiscordEventType.GuildMemberAdd:
                    var guildMemberAddDto = JsonSerializer.Deserialize<DiscordGuildMemberAddDto>(eventDataJson);
                    GuildMemberAdd?.Invoke(this, new DiscordGuildMemberAdd(guildMemberAddDto!));
                    break;
                case DiscordEventType.GuildMemberRemove:
                    var guildMemberRemoveDto = JsonSerializer.Deserialize<DiscordGuildMemberRemoveDto>(eventDataJson);
                    GuildMemberRemove?.Invoke(this, new DiscordGuildMemberRemove(guildMemberRemoveDto!));
                    break;
                case DiscordEventType.GuildMemberUpdate:
                    var guildMemberUpdateDto = JsonSerializer.Deserialize<DiscordGuildMemberUpdateDto>(eventDataJson);
                    GuildMemberUpdate?.Invoke(this, new DiscordGuildMemberUpdate(guildMemberUpdateDto!));
                    break;
                case DiscordEventType.GuildRoleCreate:
                    var guildRoleCreateDto = JsonSerializer.Deserialize<DiscordGuildRoleCreateDto>(eventDataJson);
                    GuildRoleCreate?.Invoke(this, new DiscordGuildRoleCreate(this, guildRoleCreateDto!));
                    break;
                case DiscordEventType.GuildRoleUpdate:
                    var guildRoleUpdateDto = JsonSerializer.Deserialize<DiscordGuildRoleUpdateDto>(eventDataJson);
                    GuildRoleUpdate?.Invoke(this, new DiscordGuildRoleUpdate(this, guildRoleUpdateDto!));
                    break;
                case DiscordEventType.GuildRoleDelete:
                    var guildRoleDeleteDto = JsonSerializer.Deserialize<DiscordGuildRoleDeleteDto>(eventDataJson);
                    GuildRoleDelete?.Invoke(this, new DiscordGuildRoleDelete(guildRoleDeleteDto!));
                    break;
                case DiscordEventType.InteractionCreate:
                    var interactionCreateDto = JsonSerializer.Deserialize<DiscordInteractionDto>(eventDataJson);
                    InteractionCreate?.Invoke(this, new DiscordInteraction(this, interactionCreateDto!));
                    break;
                case DiscordEventType.MessageCreate:
                    var messageCreateDto = JsonSerializer.Deserialize<DiscordMessageDto>(eventDataJson);
                    MessageCreate?.Invoke(this, new DiscordMessage(this, messageCreateDto!));
                    break;
                case DiscordEventType.MessageUpdate:
                    var messageUpdateDto = JsonSerializer.Deserialize<DiscordMessageDto>(eventDataJson);
                    MessageUpdate?.Invoke(this, new DiscordMessage(this, messageUpdateDto!));
                    break;
                case DiscordEventType.MessageDelete:
                    var messageDeleteDto = JsonSerializer.Deserialize<DiscordMessageDeleteDto>(eventDataJson);
                    MessageDelete?.Invoke(this, new DiscordMessageDelete(messageDeleteDto!));
                    break;
                case DiscordEventType.MessageDeleteBulk:
                    break;
                case DiscordEventType.MessageReactionAdd:
                    var messageReactionAddDto = JsonSerializer.Deserialize<DiscordMessageReactionAddDto>(eventDataJson);
                    MessageReactionAdd?.Invoke(this, new DiscordMessageReactionAdd(messageReactionAddDto!));
                    break;
                case DiscordEventType.MessageReactionRemove:
                    var messageReactionRemoveDto = JsonSerializer.Deserialize<DiscordMessageReactionRemoveDto>(eventDataJson);
                    MessageReactionRemove?.Invoke(this, new DiscordMessageReactionRemove(messageReactionRemoveDto!));
                    break;
                case DiscordEventType.MessageReactionRemoveAll:
                    break;
                case DiscordEventType.MessageReactionRemoveEmoji:
                    break;
                case DiscordEventType.StageInstanceCreate:
                    break;
                case DiscordEventType.StageInstanceUpdate:
                    break;
                case DiscordEventType.StageInstanceDelete:
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
                default:
                    throw new NotSupportedException($"{typeof(DiscordEventType)} {eventType} is not supported");
            }
        }
    }
}