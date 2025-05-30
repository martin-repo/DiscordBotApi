// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordShardBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Gateway.Commands;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordShardBuilder
{
	private int _numShards = default!;
	private int _shardId = default!;

	public DiscordShardBuilder WithNumShards(int numShards)
	{
		_numShards = numShards;
		return this;
	}

	public DiscordShardBuilder WithShardId(int shardId)
	{
		_shardId = shardId;
		return this;
	}

	public DiscordShard Build() =>
		new()
		{
			NumShards = _numShards,
			ShardId = _shardId,
		};
}