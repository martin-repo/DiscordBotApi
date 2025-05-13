// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRestException.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Interface.Models.Rest;

namespace DiscordBotApi.Rest;

public class DiscordRestException : ApplicationException
{
	public DiscordRestException(HttpStatusCode statusCode, DiscordErrorResponse errorResponse) : base(
		message: CreateMessage(errorResponse: errorResponse)
	)
	{
		StatusCode = statusCode;
		ErrorResponse = errorResponse;
	}

	public DiscordErrorResponse ErrorResponse { get; set; }

	public HttpStatusCode StatusCode { get; set; }

	private static string CreateMessage(DiscordErrorResponse errorResponse) =>
		$"{(int)errorResponse.Code} ({errorResponse.Code}): {errorResponse.Message}";
}