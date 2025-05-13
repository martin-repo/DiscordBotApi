// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure
internal sealed record DiscordApplicationCommandDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "application_id")]
	string ApplicationId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandOptionDto[]? Options,
	[property: JsonPropertyName(name: "default_member_permissions")]
	string? DefaultMemberPermissions,
	[property: JsonPropertyName(name: "dm_permission")]
	bool? DmPermission,
	[property: JsonPropertyName(name: "version")]
	string Version
)
{
	public DiscordApplicationCommand ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Type = Type != null ? (DiscordApplicationCommandType)Type : null,
			ApplicationId = ulong.Parse(s: ApplicationId),
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			Name = Name,
			Description = Description,
			Options = Options?.Select(selector: o => o.ToModel()).ToArray(),
			DefaultMemberPermissions =
				DefaultMemberPermissions != null ? (DiscordPermissions)ulong.Parse(s: DefaultMemberPermissions) : null,
			DmPermission = DmPermission,
			Version = ulong.Parse(s: Version)
		};
}