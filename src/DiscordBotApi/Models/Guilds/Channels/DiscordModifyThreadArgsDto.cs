// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordModifyThreadArgsDto(
	[property: JsonPropertyName(name: "archived")]
	bool? Archived
)
{
	internal DiscordModifyThreadArgsDto(DiscordModifyThreadArgs model) : this(Archived: model.Archived)
	{
	}
}