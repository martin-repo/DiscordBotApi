// -------------------------------------------------------------------------------------------------
// <copyright file="QueryBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text;

namespace DiscordBotApi.Rest;

internal class QueryBuilder
{
	private readonly StringBuilder _builder = new();

	private bool _hasQuery;

	public QueryBuilder(string pathWithoutQuery)
	{
		_builder.Append(value: pathWithoutQuery);
	}

	public void Add(string key, bool? value)
	{
		if (value == null)
		{
			return;
		}

		Add(key: key, value: value.Value.ToString());
	}

	public void Add(string key, int? value)
	{
		if (value == null)
		{
			return;
		}

		Add(key: key, value: value.Value.ToString(provider: CultureInfo.InvariantCulture));
	}

	public void Add(string key, double? value)
	{
		if (value == null)
		{
			return;
		}

		Add(key: key, value: value.Value.ToString(provider: CultureInfo.InvariantCulture));
	}

	public void Add(string key, ulong? value)
	{
		if (value == null)
		{
			return;
		}

		Add(key: key, value: value.Value.ToString());
	}

	public void Add(string key, string? value)
	{
		if (value == null)
		{
			return;
		}

		AddQuerySeparator();
		_builder.Append(handler: $"{key}={value}");
	}

	public override string ToString() => _builder.ToString();

	private void AddQuerySeparator()
	{
		if (_hasQuery)
		{
			_builder.Append(value: '&');
			return;
		}

		_builder.Append(value: '?');
		_hasQuery = true;
	}
}