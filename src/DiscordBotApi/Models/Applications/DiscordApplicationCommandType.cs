// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    // https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-types
    public enum DiscordApplicationCommandType
    {
        ChatInput = 1,
        User = 2,
        Message = 3
    }
}