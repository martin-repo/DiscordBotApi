// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGlobalApplicationCommandArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

public class DiscordCreateGlobalApplicationCommandArgsBuilder
{
	private DiscordPermissions? _defaultMemberPermissions;
	private string _description = default!;
	private bool? _dmPermission;
	private string _name = default!;
	private List<DiscordApplicationCommandOption>? _options;
	private DiscordApplicationCommandType? _type;

	public DiscordCreateGlobalApplicationCommandArgsBuilder WithDefaultMemberPermissions(DiscordPermissions? defaultMemberPermissions)
	{
		_defaultMemberPermissions = defaultMemberPermissions;
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder WithDescription(string description)
	{
		_description = description;
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder WithDmPermission(bool? dmPermission)
	{
		_dmPermission = dmPermission;
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder AddOption(Action<DiscordApplicationCommandOptionBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionBuilder();
		builderAction(obj: builder);
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder AddOption(DiscordApplicationCommandOption item)
	{
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: item);
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgsBuilder WithType(DiscordApplicationCommandType? type)
	{
		_type = type;
		return this;
	}

	public DiscordCreateGlobalApplicationCommandArgs Build() =>
		new()
		{
			DefaultMemberPermissions = _defaultMemberPermissions,
			Description = _description,
			DmPermission = _dmPermission,
			Name = _name,
			Options = _options?.ToImmutableArray(),
			Type = _type,
		};
}