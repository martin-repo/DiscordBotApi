// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

public class DiscordCreateGuildApplicationCommandArgsBuilder
{
	private DiscordPermissions? _defaultMemberPermissions;
	private string _description = default!;
	private string _name = default!;
	private List<DiscordApplicationCommandOption>? _options;
	private DiscordApplicationCommandType? _type;

	public DiscordCreateGuildApplicationCommandArgsBuilder WithDefaultMemberPermissions(DiscordPermissions? defaultMemberPermissions)
	{
		_defaultMemberPermissions = defaultMemberPermissions;
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgsBuilder WithDescription(string description)
	{
		_description = description;
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgsBuilder AddOption(Action<DiscordApplicationCommandOptionBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionBuilder();
		builderAction(obj: builder);
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgsBuilder AddOption(DiscordApplicationCommandOption item)
	{
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: item);
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgsBuilder WithType(DiscordApplicationCommandType? type)
	{
		_type = type;
		return this;
	}

	public DiscordCreateGuildApplicationCommandArgs Build() =>
		new()
		{
			DefaultMemberPermissions = _defaultMemberPermissions,
			Description = _description,
			Name = _name,
			Options = _options?.ToImmutableArray(),
			Type = _type,
		};
}