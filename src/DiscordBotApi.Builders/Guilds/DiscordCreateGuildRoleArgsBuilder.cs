// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordCreateGuildRoleArgsBuilder
{
	private string? _name;
	private DiscordPermissions? _permissions;

	public DiscordCreateGuildRoleArgsBuilder WithName(string? name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateGuildRoleArgsBuilder WithPermissions(DiscordPermissions? permissions)
	{
		_permissions = permissions;
		return this;
	}

	public DiscordCreateGuildRoleArgs Build() =>
		new()
		{
			Name = _name,
			Permissions = _permissions,
		};
}