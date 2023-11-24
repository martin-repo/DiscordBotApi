// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#modify-channel-json-params-thread
internal record DiscordModifyThreadArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "archived")]
	bool? Archived,
	[property: JsonPropertyName(name: "flags")]
	int? Flags
)
{
	internal DiscordModifyThreadArgsDto(DiscordModifyThreadArgs model) : this(
		Name: model.Name,
		Archived: model.Archived,
		Flags: (int?)model.Flags)
	{
	}
}