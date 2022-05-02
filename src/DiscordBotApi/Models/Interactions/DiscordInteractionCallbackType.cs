// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    // https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-type
    public enum DiscordInteractionCallbackType
    {
        Pong = 1,
        ChannelMessageWithSource = 4,
        DeferredChannelMessageWithSource = 5,
        DeferredUpdateMessage = 6,
        UpdateMessage = 7,
        ApplicationCommandAutocompleteResult = 8,
        Modal = 9
    }
}