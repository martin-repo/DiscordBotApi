// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommand.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Models.Guilds;

    public record DiscordApplicationCommand
    {
        internal DiscordApplicationCommand(DiscordApplicationCommandDto dto)
        {
            Id = ulong.Parse(dto.Id);
            Type = dto.Type != null ? (DiscordApplicationCommandType)dto.Type : null;
            ApplicationId = ulong.Parse(dto.ApplicationId);
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            Name = dto.Name;
            Description = dto.Description;
            Options = dto.Options?.Select(o => new DiscordApplicationCommandOption(o)).ToArray();
            DefaultMemberPermissions = (DiscordPermissions)ulong.Parse(dto.DefaultMemberPermissions);
            DmPermission = dto.DmPermission;
            Version = ulong.Parse(dto.Version);
        }

        public ulong ApplicationId { get; init; }

        public DiscordPermissions DefaultMemberPermissions { get; init; }

        public string Description { get; init; }

        public bool DmPermission { get; init; }

        public ulong? GuildId { get; init; }

        public ulong Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

        public DiscordApplicationCommandType? Type { get; init; }

        public ulong Version { get; init; }
    }
}