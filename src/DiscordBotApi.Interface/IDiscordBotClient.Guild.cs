// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Guild.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	Task AddGuildMemberRoleAsync(
		ulong guildId,
		ulong userId,
		ulong roleId,
		CancellationToken cancellationToken = default
	);

	Task<DiscordChannel> CreateGuildChannelAsync(
		ulong guildId,
		DiscordCreateGuildChannelArgs args,
		CancellationToken cancellationToken = default
	);

	Task<IReadOnlyCollection<DiscordChannel>> GetGuildChannelsAsync(
		ulong guildId,
		CancellationToken cancellationToken = default
	);

	Task RemoveGuildMemberRoleAsync(
		ulong guildId,
		ulong userId,
		ulong roleId,
		CancellationToken cancellationToken = default
	);
}