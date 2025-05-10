// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordShard.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

public sealed class DiscordShard
{
	public required int NumShards { get; init; }

	public required int ShardId { get; init; }
}