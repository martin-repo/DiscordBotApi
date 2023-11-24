// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGlobalApplicationCommandArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

internal record DiscordCreateGlobalApplicationCommandArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "options")]
	IReadOnlyCollection<DiscordApplicationCommandOptionDto>? Options,
	[property: JsonPropertyName(name: "default_member_permissions")]
	string? DefaultMemberPermissions,
	[property: JsonPropertyName(name: "dm_permission")]
	bool? DmPermission,
	[property: JsonPropertyName(name: "type")]
	int? Type
)
{
	internal DiscordCreateGlobalApplicationCommandArgsDto(DiscordCreateGlobalApplicationCommandArgs model) : this(
		Name: model.Name,
		Description: model.Description,
		Options: model.Options?.Select(selector: o => new DiscordApplicationCommandOptionDto(model: o))
			.ToArray(),
		DefaultMemberPermissions: model.DefaultMemberPermissions is not null
			? ((ulong)model.DefaultMemberPermissions).ToString()
			: null,
		DmPermission: model.DmPermission,
		Type: model.Type != null
			? (int)model.Type
			: null)
	{
	}
}