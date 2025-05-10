// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordCreateGuildRoleArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "permissions")]
	string? Permissions
)
{
	public static DiscordCreateGuildRoleArgsDto FromModel(DiscordCreateGuildRoleArgs model) =>
		new(Name: model.Name, Permissions: model.Permissions != null ? ((ulong)model.Permissions).ToString() : null);
}