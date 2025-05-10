// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionPropertiesDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal sealed record DiscordGatewayConnectionPropertiesDto(
	[property: JsonPropertyName(name: "$os")]
	string OperatingSystem,
	[property: JsonPropertyName(name: "$browser")]
	string BrowserName,
	[property: JsonPropertyName(name: "$device")]
	string DeviceName
)
{
	public static DiscordGatewayConnectionPropertiesDto FromModel(DiscordGatewayConnectionProperties model) =>
		new(OperatingSystem: model.OperatingSystem, BrowserName: model.BrowserName, DeviceName: model.DeviceName);
}