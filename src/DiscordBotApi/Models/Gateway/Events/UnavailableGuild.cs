// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuild.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    public record UnavailableGuild
    {
        internal UnavailableGuild(UnavailableGuildDto dto)
        {
            Id = ulong.Parse(dto.Id);
            Unavailable = dto.Unavailable;
        }

        public ulong Id { get; init; }

        public bool? Unavailable { get; init; }
    }
}