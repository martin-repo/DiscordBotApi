// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordModifyGuildRoleArgsBuilder
{
	private string? _name;
	private DiscordPermissions? _permissions;

	public DiscordModifyGuildRoleArgsBuilder WithName(string? name)
	{
		_name = name;
		return this;
	}

	public DiscordModifyGuildRoleArgsBuilder WithPermissions(DiscordPermissions? permissions)
	{
		_permissions = permissions;
		return this;
	}

	public DiscordModifyGuildRoleArgs Build() =>
		new()
		{
			Name = _name,
			Permissions = _permissions,
		};
}