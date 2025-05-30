// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuOptionBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Components;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordMessageSelectMenuOptionBuilder
{
	private bool? _default;
	private string? _description;
	private DiscordEmoji? _emoji;
	private string _label = default!;
	private string _value = default!;

	public DiscordMessageSelectMenuOptionBuilder WithDefault(bool? @default)
	{
		_default = @default;
		return this;
	}

	public DiscordMessageSelectMenuOptionBuilder WithDescription(string? description)
	{
		_description = description;
		return this;
	}

	public DiscordMessageSelectMenuOptionBuilder WithEmoji(DiscordEmoji? emoji)
	{
		_emoji = emoji;
		return this;
	}

	public DiscordMessageSelectMenuOptionBuilder WithLabel(string label)
	{
		_label = label;
		return this;
	}

	public DiscordMessageSelectMenuOptionBuilder WithValue(string value)
	{
		_value = value;
		return this;
	}

	public DiscordMessageSelectMenuOption Build() =>
		new()
		{
			Default = _default,
			Description = _description,
			Emoji = _emoji,
			Label = _label,
			Value = _value,
		};
}