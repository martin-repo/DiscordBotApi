// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplication.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Applications;

public record DiscordApplication
{
	internal DiscordApplication(DiscordApplicationDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		Name = dto.Name;
		BotPublic = dto.BotPublic;
		BotRequireCodeGrant = dto.BotRequireCodeGrant;
		Owner = dto.Owner != null
			? new DiscordUser(dto: dto.Owner)
			: null;
		Flags = dto.Flags != null
			? (DiscordApplicationFlags)dto.Flags
			: null;
		Tags = dto.Tags;
		InstallParams = dto.InstallParams != null
			? new DiscordInstallParams(dto: dto.InstallParams)
			: null;
		CustomInstallUrl = dto.CustomInstallUrl;
	}

	public bool BotPublic { get; init; }

	public bool BotRequireCodeGrant { get; init; }

	public IReadOnlyCollection<string>? CustomInstallUrl { get; init; }

	public DiscordApplicationFlags? Flags { get; init; }

	public ulong Id { get; init; }

	public DiscordInstallParams? InstallParams { get; init; }

	public string Name { get; init; }

	public DiscordUser? Owner { get; init; }

	public IReadOnlyCollection<string>? Tags { get; init; }
}