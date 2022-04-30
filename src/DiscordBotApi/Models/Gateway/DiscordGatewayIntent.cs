// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayIntent.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    // https://discord.com/developers/docs/topics/gateway#list-of-intents
    [Flags]
    public enum DiscordGatewayIntent
    {
        None = 0,
        Guilds = 1 << 0,
        GuildMembers = 1 << 1,
        GuildMessages = 1 << 9,
        GuildMessageReactions = 1 << 10
    }
}