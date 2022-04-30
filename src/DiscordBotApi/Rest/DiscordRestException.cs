// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRestException.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using System.Net;

    using DiscordBotApi.Models.Rest;

    public class DiscordRestException : ApplicationException
    {
        public DiscordRestException(HttpStatusCode statusCode, DiscordErrorResponse errorResponse)
            : base(CreateMessage(errorResponse))
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
        }

        public DiscordErrorResponse ErrorResponse { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        private static string CreateMessage(DiscordErrorResponse errorResponse)
        {
            return $"{(int)errorResponse.Code} ({errorResponse.Code}): {errorResponse.Message}";
        }
    }
}