// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwriteDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordPermissionOverwriteDto(
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
	public static DiscordPermissionOverwriteDto FromModel(DiscordPermissionOverwrite model) =>
		new(
			Id: model.Id.ToString(),
			Type: (int)model.Type,
			Allow: ((ulong)model.Allow).ToString(),
			Deny: ((ulong)model.Deny).ToString()
		);

	public DiscordPermissionOverwrite ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Type = (DiscordPermissionOverwriteType)Type,
			Allow = (DiscordPermissions)ulong.Parse(s: Allow),
			Deny = (DiscordPermissions)ulong.Parse(s: Deny)
		};
}