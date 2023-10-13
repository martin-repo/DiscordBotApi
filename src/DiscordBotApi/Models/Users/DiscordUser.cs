// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUser.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Users;

public record DiscordUser
{
	internal DiscordUser(DiscordUserDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		Username = dto.Username;
		Discriminator = dto.Discriminator;
		GlobalName = dto.GlobalName;
		Avatar = dto.Avatar;
		Bot = dto.Bot;
	}

	public string? Avatar { get; init; }

	public bool? Bot { get; init; }

	public string Discriminator { get; init; }

	public string? GlobalName { get; init; }

	public ulong Id { get; init; }

	public string Username { get; init; }
}