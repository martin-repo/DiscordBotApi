// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordErrorResponseDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Rest;
using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Models.Rest;

internal sealed record DiscordErrorResponseDto(
	[property: JsonPropertyName(name: "code")]
	int Code,
	[property: JsonPropertyName(name: "message")]
	string Message,
	[property: JsonPropertyName(name: "errors")]
	JsonData? JsonKey
)
{
	public DiscordErrorResponse ToModel() =>
		new()
		{
			Code = (DiscordJsonErrorCode)Code,
			Message = Message,
			JsonKey = JsonKey?.Json
		};
}