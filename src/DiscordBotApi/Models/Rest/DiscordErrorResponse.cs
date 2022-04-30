// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordErrorResponse.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    public record DiscordErrorResponse
    {
        internal DiscordErrorResponse(DiscordJsonErrorCode code, string message, string jsonKey)
        {
            Code = code;
            Message = message;
            JsonKey = jsonKey;
        }

        internal DiscordErrorResponse(DiscordErrorResponseDto dto)
        {
            Code = (DiscordJsonErrorCode)dto.Code;
            Message = dto.Message;
            JsonKey = dto.JsonKey?.Json;
        }

        public DiscordJsonErrorCode Code { get; }

        public string? JsonKey { get; }

        public string Message { get; }
    }
}