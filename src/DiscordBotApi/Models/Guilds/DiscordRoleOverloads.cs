// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRoleOverloads.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public partial record DiscordRole
    {
        public async Task<DiscordRole> ModifyAsync(string name)
        {
            return await _botClient.ModifyGuildRoleAsync(_guildId, Id, new DiscordModifyGuildRoleArgs { Name = name }).ConfigureAwait(false);
        }

        public async Task<DiscordRole> ModifyAsync(DiscordPermissions permissions)
        {
            return await _botClient.ModifyGuildRoleAsync(_guildId, Id, new DiscordModifyGuildRoleArgs { Permissions = permissions })
                                   .ConfigureAwait(false);
        }

        public async Task<DiscordRole> ModifyAsync(string name, DiscordPermissions permissions)
        {
            return await _botClient.ModifyGuildRoleAsync(_guildId, Id, new DiscordModifyGuildRoleArgs { Name = name, Permissions = permissions })
                                   .ConfigureAwait(false);
        }
    }
}