// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordStartThreadWithMessageArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "auto_archive_duration")]
	int? AutoArchiveDuration
)
{
	internal DiscordStartThreadWithMessageArgsDto(DiscordStartThreadWithMessageArgs model) : this(
		Name: model.Name,
		AutoArchiveDuration: model.AutoArchiveDuration != null
			? (int)model.AutoArchiveDuration
			: null)
	{
	}
}