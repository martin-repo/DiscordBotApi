// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReady.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Applications;
    using DiscordBotApi.Models.Gateway.Commands;
    using DiscordBotApi.Models.Users;

    public record DiscordReady
    {
        internal DiscordReady(DiscordReadyDto dto)
        {
            V = dto.V;
            User = new DiscordUser(dto.User);
            Guilds = dto.Guilds.Select(g => new UnavailableGuild(g)).ToArray();
            SessionId = dto.SessionId;
            Shard = dto.Shard != null ? new DiscordShard { ShardId = dto.Shard[0], NumShards = dto.Shard[1] } : null;
            Application = new DiscordApplication(dto.Application);
        }

        public DiscordApplication Application { get; init; }

        public IReadOnlyCollection<UnavailableGuild> Guilds { get; init; }

        public string SessionId { get; init; }

        public DiscordShard? Shard { get; init; }

        public DiscordUser User { get; init; }

        public int V { get; init; }
    }
}