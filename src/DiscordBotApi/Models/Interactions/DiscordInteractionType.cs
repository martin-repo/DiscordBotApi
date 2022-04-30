// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    public enum DiscordInteractionType
    {
        Ping = 1,
        ApplicationCommand = 2,
        MessageComponent = 3,
        ApplicaitonCommandAutocomplete = 4,
        ModalSubmit = 5
    }
}