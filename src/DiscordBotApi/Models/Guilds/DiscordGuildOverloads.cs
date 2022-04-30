// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildOverloads.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using DiscordBotApi.Models.Guilds.Channels;

    public partial record DiscordGuild
    {
        public async Task<DiscordChannel> CreateChannelAsync(string name, DiscordChannelType type)
        {
            return await _botClient.CreateGuildChannelAsync(Id, new DiscordCreateGuildChannelArgs { Name = name, Type = type }).ConfigureAwait(false);
        }
    }
}