// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordCreateGuildEmojiArgsBuilder
{
	private string _filePath = default!;
	private string _name = default!;

	public DiscordCreateGuildEmojiArgsBuilder WithFilePath(string filePath)
	{
		_filePath = filePath;
		return this;
	}

	public DiscordCreateGuildEmojiArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateGuildEmojiArgs Build() =>
		new()
		{
			FilePath = _filePath,
			Name = _name,
		};
}