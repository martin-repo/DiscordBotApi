// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordShardBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Gateway.Commands;

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