// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordShard.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public record DiscordShard
{
	public int NumShards { get; init; }

	public int ShardId { get; init; }
}