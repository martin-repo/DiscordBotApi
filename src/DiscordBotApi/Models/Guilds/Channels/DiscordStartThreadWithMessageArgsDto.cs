// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

internal sealed record DiscordStartThreadWithMessageArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "auto_archive_duration")]
	int? AutoArchiveDuration
)
{
	public static DiscordStartThreadWithMessageArgsDto FromModel(DiscordStartThreadWithMessageArgs model) =>
		new(
			Name: model.Name,
			AutoArchiveDuration: model.AutoArchiveDuration
		);
}