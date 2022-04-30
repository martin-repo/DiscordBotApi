// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdate.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    public record DiscordPresenceUpdate
    {
        public IReadOnlyCollection<DiscordActivityUpdate> Activities { get; init; } = Array.Empty<DiscordActivityUpdate>();

        public bool Afk { get; init; }

        public DateTime? Since { get; init; }

        public DiscordPresenceStatus Status { get; init; }
    }
}