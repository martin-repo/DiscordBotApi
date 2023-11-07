// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwriteBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordPermissionOverwriteBuilder
{
	private DiscordPermissions _allow = default!;
	private DiscordPermissions _deny = default!;
	private ulong _id = default!;
	private DiscordPermissionOverwriteType _type = default!;

	public DiscordPermissionOverwriteBuilder WithAllow(DiscordPermissions allow)
	{
		_allow = allow;
		return this;
	}

	public DiscordPermissionOverwriteBuilder WithDeny(DiscordPermissions deny)
	{
		_deny = deny;
		return this;
	}

	public DiscordPermissionOverwriteBuilder WithId(ulong id)
	{
		_id = id;
		return this;
	}

	public DiscordPermissionOverwriteBuilder WithType(DiscordPermissionOverwriteType type)
	{
		_type = type;
		return this;
	}

	public DiscordPermissionOverwrite Build() =>
		new()
		{
			Allow = _allow,
			Deny = _deny,
			Id = _id,
			Type = _type,
		};
}