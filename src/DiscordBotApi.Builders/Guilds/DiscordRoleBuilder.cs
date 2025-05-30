// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRoleBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordRoleBuilder
{
	private ulong _id = default!;
	private string _name = default!;
	private DiscordPermissions _permissions = default!;

	public DiscordRoleBuilder WithId(ulong id)
	{
		_id = id;
		return this;
	}

	public DiscordRoleBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordRoleBuilder WithPermissions(DiscordPermissions permissions)
	{
		_permissions = permissions;
		return this;
	}

	public DiscordRole Build() =>
		new()
		{
			Id = _id,
			Name = _name,
			Permissions = _permissions,
		};
}