// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuildDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/resources/guild#unavailable-guild-object
internal sealed record UnavailableGuildDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "unavailable")]
	bool? Unavailable
)
{
	public UnavailableGuild ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Unavailable = Unavailable
		};
}