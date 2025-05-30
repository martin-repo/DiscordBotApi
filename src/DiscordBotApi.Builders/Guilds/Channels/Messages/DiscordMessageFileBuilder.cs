// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFileBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
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