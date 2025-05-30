// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgsBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
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