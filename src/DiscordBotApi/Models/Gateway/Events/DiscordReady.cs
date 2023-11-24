// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReady.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Gateway.Commands;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordReady
{
	internal DiscordReady(DiscordReadyDto dto)
	{
		V = dto.V;
		User = new DiscordUser(dto: dto.User);
		Guilds = dto.Guilds.Select(selector: g => new UnavailableGuild(dto: g))
			.ToArray();
		SessionId = dto.SessionId;
		ResumeGatewayUrl = dto.ResumeGatewayUrl;
		Shard = dto.Shard != null
			? new DiscordShard
			{
				ShardId = dto.Shard[0],
				NumShards = dto.Shard[1]
			}
			: null;
		Application = new DiscordApplication(dto: dto.Application);
	}

	public DiscordApplication Application { get; init; }

	public IReadOnlyCollection<UnavailableGuild> Guilds { get; init; }

	public string ResumeGatewayUrl { get; init; }

	public string SessionId { get; init; }

	public DiscordShard? Shard { get; init; }

	public DiscordUser User { get; init; }

	public int V { get; init; }
}