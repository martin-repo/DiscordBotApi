// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResumeDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    internal record DiscordResumeDto(
        [property: JsonPropertyName("token")] string Token,
        [property: JsonPropertyName("session_id")] string SessionId,
        [property: JsonPropertyName("seq")] int SequenceNumber);
}