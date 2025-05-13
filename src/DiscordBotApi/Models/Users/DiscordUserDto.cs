// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUserDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Models.Users;

internal sealed class DiscordUserDto
{
	[JsonPropertyName(name: "avatar")]
	public string? Avatar { get; init; }

	[JsonPropertyName(name: "bot")]
	public bool? Bot { get; init; }

	[JsonPropertyName(name: "discriminator")]
	public required string Discriminator { get; init; }

	[JsonPropertyName(name: "global_name")]
	public string? GlobalName { get; init; }

	[JsonPropertyName(name: "id")]
	public required string Id { get; init; }

	[JsonPropertyName(name: "username")]
	public required string Username { get; init; }

	internal DiscordUser ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Username = Username,
			Discriminator = Discriminator,
			GlobalName = GlobalName,
			Avatar = Avatar,
			Bot = Bot
		};
}