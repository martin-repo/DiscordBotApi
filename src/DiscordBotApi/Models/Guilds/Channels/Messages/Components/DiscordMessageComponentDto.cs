// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(DiscordMessageComponentDtoConverter))]
    internal abstract record DiscordMessageComponentDto([property: JsonPropertyName("type")] int Type)
    {
        internal static DiscordMessageComponent ConvertToModel(DiscordMessageComponentDto dto)
        {
            switch (dto)
            {
                case DiscordMessageActionRowDto actionRowDto:
                    return new DiscordMessageActionRow(actionRowDto);
                case DiscordMessageButtonDto buttonDto:
                    return new DiscordMessageButton(buttonDto);
                default:
                    throw new NotSupportedException($"{typeof(DiscordMessageComponent)} {dto.GetType().Name} is not supported");
            }
        }
    }
}