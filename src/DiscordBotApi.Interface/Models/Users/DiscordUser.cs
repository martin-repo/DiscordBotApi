// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUser.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Users;

public sealed class DiscordUser
{
	public string? Avatar { get; init; }

	public bool? Bot { get; init; }

	public required string Discriminator { get; init; }

	public string? GlobalName { get; init; }

	public required ulong Id { get; init; }

	public required string Username { get; init; }
}