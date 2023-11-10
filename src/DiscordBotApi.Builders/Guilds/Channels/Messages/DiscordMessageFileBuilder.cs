// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFileBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordMessageFileBuilder
{
	private string _filePath = default!;
	private ulong _id = default!;

	public DiscordMessageFileBuilder WithFilePath(string filePath)
	{
		_filePath = filePath;
		return this;
	}

	public DiscordMessageFileBuilder WithId(ulong id)
	{
		_id = id;
		return this;
	}

	public DiscordMessageFile Build() =>
		new()
		{
			FilePath = _filePath,
			Id = _id,
		};
}