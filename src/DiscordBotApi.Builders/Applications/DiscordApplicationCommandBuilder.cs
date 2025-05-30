// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordApplicationCommandBuilder
{
	private ulong _applicationId = default!;
	private DiscordPermissions? _defaultMemberPermissions;
	private string _description = default!;
	private bool? _dmPermission;
	private ulong? _guildId;
	private ulong _id = default!;
	private string _name = default!;
	private List<DiscordApplicationCommandOption>? _options;
	private DiscordApplicationCommandType? _type;
	private ulong _version = default!;

	public DiscordApplicationCommandBuilder WithApplicationId(ulong applicationId)
	{
		_applicationId = applicationId;
		return this;
	}

	public DiscordApplicationCommandBuilder WithDefaultMemberPermissions(DiscordPermissions? defaultMemberPermissions)
	{
		_defaultMemberPermissions = defaultMemberPermissions;
		return this;
	}

	public DiscordApplicationCommandBuilder WithDescription(string description)
	{
		_description = description;
		return this;
	}

	public DiscordApplicationCommandBuilder WithDmPermission(bool? dmPermission)
	{
		_dmPermission = dmPermission;
		return this;
	}

	public DiscordApplicationCommandBuilder WithGuildId(ulong? guildId)
	{
		_guildId = guildId;
		return this;
	}

	public DiscordApplicationCommandBuilder WithId(ulong id)
	{
		_id = id;
		return this;
	}

	public DiscordApplicationCommandBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordApplicationCommandBuilder AddOption(Action<DiscordApplicationCommandOptionBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionBuilder();
		builderAction(obj: builder);
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: builder.Build());
		return this;
	}

	public DiscordApplicationCommandBuilder AddOption(DiscordApplicationCommandOption item)
	{
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: item);
		return this;
	}

	public DiscordApplicationCommandBuilder WithType(DiscordApplicationCommandType? type)
	{
		_type = type;
		return this;
	}

	public DiscordApplicationCommandBuilder WithVersion(ulong version)
	{
		_version = version;
		return this;
	}

	public DiscordApplicationCommand Build() =>
		new()
		{
			ApplicationId = _applicationId,
			DefaultMemberPermissions = _defaultMemberPermissions,
			Description = _description,
			DmPermission = _dmPermission,
			GuildId = _guildId,
			Id = _id,
			Name = _name,
			Options = _options?.ToImmutableArray(),
			Type = _type,
			Version = _version,
		};
}