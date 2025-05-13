// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordGetGuildArgsDto(
	[property: JsonPropertyName(name: "with_counts")]
	bool? WithCounts
)
{
	public static DiscordGetGuildArgsDto FromModel(DiscordGetGuildArgs model) => new(WithCounts: model.WithCounts);
}