// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordErrorResponseDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Gateway;

    internal record DiscordErrorResponseDto(
        [property: JsonPropertyName("code")] int Code,
        [property: JsonPropertyName("message")] string Message,
        [property: JsonPropertyName("errors")] JsonData? JsonKey);
}