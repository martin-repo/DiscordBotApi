// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure
internal sealed class DiscordInteractionDto
{
	[JsonPropertyName(name: "application_id")]
	public required string ApplicationId { get; init; }

	[JsonPropertyName(name: "channel_id")]
	public string? ChannelId { get; init; }

	[JsonPropertyName(name: "data")]
	public DiscordInteractionDataDto? Data { get; init; }

	[JsonPropertyName(name: "guild_id")]
	public string? GuildId { get; init; }

	[JsonPropertyName(name: "id")]
	public required string Id { get; init; }

	[JsonPropertyName(name: "member")]
	public DiscordGuildMemberDto? Member { get; init; }

	[JsonPropertyName(name: "message")]
	public DiscordMessageDto? Message { get; init; }

	[JsonPropertyName(name: "token")]
	public required string Token { get; init; }

	[JsonPropertyName(name: "type")]
	public required int Type { get; init; }

	[JsonPropertyName(name: "user")]
	public DiscordUserDto? User { get; init; }

	public DiscordInteraction ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			ApplicationId = ulong.Parse(s: ApplicationId),
			Type = (DiscordInteractionType)Type,
			Data = Data?.ToModel(),
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			ChannelId = ChannelId != null ? ulong.Parse(s: ChannelId) : null,
			Member = Member?.ToModel(),
			User = User?.ToModel(),
			Token = Token,
			Message = Message?.ToModel()
		};
}