// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplication.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Models.Users;

    public record DiscordApplication
    {
        internal DiscordApplication(DiscordApplicationDto dto)
        {
            Id = ulong.Parse(dto.Id);
            Name = dto.Name;
            BotPublic = dto.BotPublic;
            BotRequireCodeGrant = dto.BotRequireCodeGrant;
            Owner = dto.Owner != null ? new DiscordUser(dto.Owner) : null;
            Flags = dto.Flags != null ? (DiscordApplicationFlags)dto.Flags : null;
            Tags = dto.Tags;
            InstallParams = dto.InstallParams != null ? new DiscordInstallParams(dto.InstallParams) : null;
            CustomInstallUrl = dto.CustomInstallUrl;
        }

        public bool BotPublic { get; init; }

        public bool BotRequireCodeGrant { get; init; }

        public IReadOnlyCollection<string>? CustomInstallUrl { get; init; }

        public DiscordApplicationFlags? Flags { get; init; }

        public ulong Id { get; init; }

        public DiscordInstallParams? InstallParams { get; init; }

        public string Name { get; init; }

        public DiscordUser? Owner { get; init; }

        public IReadOnlyCollection<string>? Tags { get; init; }
    }
}