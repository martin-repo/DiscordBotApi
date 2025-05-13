// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;

namespace DiscordBotApi.Models.Applications;

internal sealed record DiscordCreateGuildApplicationCommandArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "options")]
	IReadOnlyCollection<DiscordApplicationCommandOptionDto>? Options,
	[property: JsonPropertyName(name: "default_member_permissions")]
	string? DefaultMemberPermissions,
	[property: JsonPropertyName(name: "type")]
	int? Type
)
{
	public static DiscordCreateGuildApplicationCommandArgsDto
		FromModel(DiscordCreateGuildApplicationCommandArgs model) =>
		new(
			Name: model.Name,
			Description: model.Description,
			Options: model.Options?.Select(selector: DiscordApplicationCommandOptionDto.FromModel).ToArray(),
			DefaultMemberPermissions: model.DefaultMemberPermissions is not null
				? ((ulong)model.DefaultMemberPermissions).ToString()
				: null,
			Type: model.Type is not null ? (int)model.Type : null
		);
}