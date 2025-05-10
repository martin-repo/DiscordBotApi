// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmoji.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Emojis;

public sealed class DiscordEmoji
{
	public bool? Animated { get; init; }

	public bool? Available { get; init; }

	public ulong? Id { get; init; }

	public string? Name { get; init; }

	public bool? RequireColons { get; init; }
}