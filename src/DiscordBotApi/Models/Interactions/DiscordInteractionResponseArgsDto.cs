// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json.Serialization;

    internal record DiscordInteractionResponseArgsDto(
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("data")] DiscordInteractionCallbackDataDto? Data)
    {
        internal DiscordInteractionResponseArgsDto(DiscordInteractionResponseArgs model)
            : this((int)model.Type, ConvertToDto(model.Data))
        {
        }

        private static DiscordInteractionCallbackDataDto? ConvertToDto(DiscordInteractionCallbackData? model)
        {
            if (model == null)
            {
                return null;
            }

            switch (model)
            {
                case DiscordInteractionCallbackMessage message:
                    return new DiscordInteractionCallbackMessageDto(message);
                default:
                    throw new NotSupportedException($"{typeof(DiscordInteractionCallbackData)} {model.GetType().Name} is not supported");
            }
        }
    }
}