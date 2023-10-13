// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRole.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordRole
{
	private readonly DiscordBotClient _botClient;
	private readonly ulong _guildId;

	internal DiscordRole(DiscordBotClient botClient, ulong guildId, DiscordRoleDto dto)
	{
		_botClient = botClient;
		_guildId = guildId;

		Id = ulong.Parse(s: dto.Id);
		Name = dto.Name;
		Permissions = (DiscordPermissions)ulong.Parse(s: dto.Permissions);
	}

	public ulong Id { get; }

	public string Name { get; }

	public DiscordPermissions Permissions { get; }

	public async Task DeleteAsync() =>
		await _botClient.DeleteGuildRoleAsync(guildId: _guildId, roleId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordRole> ModifyAsync(DiscordModifyGuildRoleArgs args) =>
		await _botClient.ModifyGuildRoleAsync(guildId: _guildId, roleId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);
}