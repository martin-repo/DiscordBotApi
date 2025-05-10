// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReadyDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#ready-ready-event-fields
internal sealed record DiscordReadyDto(
	[property: JsonPropertyName(name: "v")]
	int V,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User,
	[property: JsonPropertyName(name: "guilds")]
	UnavailableGuildDto[] Guilds,
	[property: JsonPropertyName(name: "session_id")]
	string SessionId,
	[property: JsonPropertyName(name: "resume_gateway_url")]
	string ResumeGatewayUrl,
	[property: JsonPropertyName(name: "shard")]
	int[]? Shard,
	[property: JsonPropertyName(name: "application")]
	DiscordApplicationDto Application
)
{
	public DiscordReady ToModel() =>
		new()
		{
			V = V,
			User = User.ToModel(),
			Guilds = Guilds.Select(selector: g => g.ToModel()).ToArray(),
			SessionId = SessionId,
			ResumeGatewayUrl = ResumeGatewayUrl,
			Shard = Shard != null
				? new DiscordShard
				{
					ShardId = Shard[0],
					NumShards = Shard[1]
				}
				: null,
			Application = Application.ToModel()
		};
}