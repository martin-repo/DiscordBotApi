// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordModifyGuildRoleArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "permissions")]
	string? Permissions
)
{
	public static DiscordModifyGuildRoleArgsDto FromModel(DiscordModifyGuildRoleArgs model) =>
		new(Name: model.Name, Permissions: model.Permissions != null ? ((ulong)model.Permissions).ToString() : null);
}