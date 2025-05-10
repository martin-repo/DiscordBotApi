// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway;

namespace DiscordBotApi.Models.Gateway;

internal sealed record DiscordGatewayDto(
	[property: JsonPropertyName(name: "url")]
	string Url
)
{
	public DiscordGateway ToModel() => new() { Url = Url };
}