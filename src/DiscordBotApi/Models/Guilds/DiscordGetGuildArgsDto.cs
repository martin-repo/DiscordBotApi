// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordGetGuildArgsDto(
	[property: JsonPropertyName(name: "with_counts")]
	bool? WithCounts
)
{
	internal DiscordGetGuildArgsDto(DiscordGetGuildArgs model) : this(WithCounts: model.WithCounts)
	{
	}
}