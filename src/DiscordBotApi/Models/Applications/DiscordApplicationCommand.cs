// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommand.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Applications;

public record DiscordApplicationCommand
{
	internal DiscordApplicationCommand(DiscordApplicationCommandDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		Type = dto.Type != null
			? (DiscordApplicationCommandType)dto.Type
			: null;
		ApplicationId = ulong.Parse(s: dto.ApplicationId);
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Name = dto.Name;
		Description = dto.Description;
		Options = dto.Options?.Select(selector: o => new DiscordApplicationCommandOption(dto: o))
			.ToArray();
		DefaultMemberPermissions = dto.DefaultMemberPermissions != null
			? (DiscordPermissions)ulong.Parse(s: dto.DefaultMemberPermissions)
			: null;
		DmPermission = dto.DmPermission;
		Version = ulong.Parse(s: dto.Version);
	}

	public ulong ApplicationId { get; init; }

	public DiscordPermissions? DefaultMemberPermissions { get; init; }

	public string Description { get; init; }

	public bool? DmPermission { get; init; }

	public ulong? GuildId { get; init; }

	public ulong Id { get; init; }

	public string Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

	public DiscordApplicationCommandType? Type { get; init; }

	public ulong Version { get; init; }
}