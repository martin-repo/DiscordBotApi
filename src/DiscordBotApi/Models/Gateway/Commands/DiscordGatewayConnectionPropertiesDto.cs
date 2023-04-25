// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionPropertiesDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordGatewayConnectionPropertiesDto(
	[property: JsonPropertyName(name: "$os")]
	string OperatingSystem,
	[property: JsonPropertyName(name: "$browser")]
	string BrowserName,
	[property: JsonPropertyName(name: "$device")]
	string DeviceName
)
{
	internal DiscordGatewayConnectionPropertiesDto(DiscordGatewayConnectionProperties model) : this(
		OperatingSystem: model.OperatingSystem,
		BrowserName: model.BrowserName,
		DeviceName: model.DeviceName)
	{
	}
}