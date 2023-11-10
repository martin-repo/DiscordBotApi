// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdateBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Gateway.Commands;

public class DiscordActivityUpdateBuilder
{
	private string _name = default!;
	private DiscordActivityType _type = default!;
	private string? _url;

	public DiscordActivityUpdateBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordActivityUpdateBuilder WithType(DiscordActivityType type)
	{
		_type = type;
		return this;
	}

	public DiscordActivityUpdateBuilder WithUrl(string? url)
	{
		_url = url;
		return this;
	}

	public DiscordActivityUpdate Build() =>
		new()
		{
			Name = _name,
			Type = _type,
			Url = _url,
		};
}