// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public enum DiscordChannelType
    {
        GuildText = 0,
        Dm = 1,
        GuildVoice = 2,
        GroupDm = 3,
        GuildCategory = 4,
        GuildNews = 5,
        GuildStore = 6,
        GuildNewsThread = 10,
        GuildPublicThread = 11,
        GuildPrivateThread = 12,
        GuildStageVoice = 13,
        GuildDirectory = 14
    }
}