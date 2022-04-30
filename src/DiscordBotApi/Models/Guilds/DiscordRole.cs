// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRole.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public partial record DiscordRole
    {
        private readonly DiscordBotClient _botClient;
        private readonly ulong _guildId;

        internal DiscordRole(DiscordBotClient botClient, ulong guildId, DiscordRoleDto dto)
        {
            _botClient = botClient;
            _guildId = guildId;

            Id = ulong.Parse(dto.Id);
            Name = dto.Name;
            Permissions = (DiscordPermissions)ulong.Parse(dto.Permissions);
        }

        public ulong Id { get; }

        public string Name { get; }

        public DiscordPermissions Permissions { get; }

        public async Task DeleteAsync()
        {
            await _botClient.DeleteGuildRoleAsync(_guildId, Id).ConfigureAwait(false);
        }

        public async Task<DiscordRole> ModifyAsync(DiscordModifyGuildRoleArgs args)
        {
            return await _botClient.ModifyGuildRoleAsync(_guildId, Id, args).ConfigureAwait(false);
        }
    }
}