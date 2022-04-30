// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponent.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    public abstract record DiscordMessageComponent
    {
        internal static DiscordMessageComponentDto ConvertToDto(DiscordMessageComponent model)
        {
            switch (model)
            {
                case DiscordMessageActionRow actionRow:
                    return new DiscordMessageActionRowDto(actionRow);
                case DiscordMessageButton button:
                    return new DiscordMessageButtonDto(button);
                default:
                    throw new NotSupportedException($"{typeof(DiscordMessageComponent)} {model.GetType().Name} is not supported");
            }
        }
    }
}