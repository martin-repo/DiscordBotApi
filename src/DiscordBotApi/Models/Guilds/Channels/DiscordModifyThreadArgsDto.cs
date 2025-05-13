// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#modify-channel-json-params-thread
internal sealed record DiscordModifyThreadArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "archived")]
	bool? Archived,
	[property: JsonPropertyName(name: "flags")]
	int? Flags
)
{
	public static DiscordModifyThreadArgsDto FromModel(DiscordModifyThreadArgs model) =>
		new(Name: model.Name, Archived: model.Archived, Flags: (int?)model.Flags);
}