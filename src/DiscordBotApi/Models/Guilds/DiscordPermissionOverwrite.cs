// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwrite.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordPermissionOverwrite()
{
	internal DiscordPermissionOverwrite(DiscordPermissionOverwriteDto dto) : this()
	{
		Id = ulong.Parse(s: dto.Id);
		Type = (DiscordPermissionOverwriteType)dto.Type;
		Allow = (DiscordPermissions)ulong.Parse(s: dto.Allow);
		Deny = (DiscordPermissions)ulong.Parse(s: dto.Deny);
	}

	public DiscordPermissions Allow { get; init; }

	public DiscordPermissions Deny { get; init; }

	public ulong Id { get; init; }

	public DiscordPermissionOverwriteType Type { get; init; }
}