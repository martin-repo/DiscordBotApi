// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRow.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    public record DiscordMessageActionRow() : DiscordMessageComponent
    {
        internal DiscordMessageActionRow(DiscordMessageActionRowDto dto)
            : this()
        {
            Components = dto.Components.Select(DiscordMessageComponentDto.ConvertToModel).ToArray();
        }

        public IReadOnlyCollection<DiscordMessageComponent> Components { get; set; } = Array.Empty<DiscordMessageComponent>();
    }
}