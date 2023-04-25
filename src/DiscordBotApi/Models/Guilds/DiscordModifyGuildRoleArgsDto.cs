// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordModifyGuildRoleArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "permissions")]
	string? Permissions
)
{
	internal DiscordModifyGuildRoleArgsDto(DiscordModifyGuildRoleArgs model) : this(
		Name: model.Name,
		Permissions: model.Permissions != null
			? ((ulong)model.Permissions).ToString()
			: null)
	{
	}
}