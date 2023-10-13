// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwriteDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordPermissionOverwriteDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "allow")]
	string Allow,
	[property: JsonPropertyName(name: "deny")]
	string Deny
)
{
	internal DiscordPermissionOverwriteDto(DiscordPermissionOverwrite model) : this(
		Id: model.Id.ToString(),
		Type: (int)model.Type,
		Allow: ((ulong)model.Allow).ToString(),
		Deny: ((ulong)model.Deny).ToString())
	{
	}
}