// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordField.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordField()
    {
        internal DiscordField(DiscordFieldDto dto)
            : this()
        {
            Name = dto.Name;
            Value = dto.Value;
            Inline = dto.Inline;
        }

        public bool? Inline { get; init; }

        public string Name { get; init; } = "";

        public string Value { get; init; } = "";
    }
}