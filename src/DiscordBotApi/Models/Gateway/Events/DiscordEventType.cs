// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEventType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    // https://discord.com/developers/docs/topics/gateway#commands-and-events
    internal enum DiscordEventType
    {
        ApplicationCommandPermissionsUpdate,
        ChannelCreate,
        ChannelUpdate,
        ChannelDelete,
        ChannelPinsUpdate,
        GuildCreate,
        GuildUpdate,
        GuildDelete,
        GuildMemberAdd,
        GuildMemberRemove,
        GuildMemberUpdate,
        GuildRoleCreate,
        GuildRoleUpdate,
        GuildRoleDelete,
        InteractionCreate,
        MessageCreate,
        MessageUpdate,
        MessageDelete,
        MessageDeleteBulk,
        MessageReactionAdd,
        MessageReactionRemove,
        MessageReactionRemoveAll,
        MessageReactionRemoveEmoji,
        Ready,
        Resumed,
        StageInstanceCreate,
        StageInstanceUpdate,
        StageInstanceDelete,
        ThreadCreate,
        ThreadUpdate,
        ThreadDelete,
        ThreadListSync,
        ThreadMemberUpdate,
        ThreadMembersUpdate,
        UserUpdate
    }
}