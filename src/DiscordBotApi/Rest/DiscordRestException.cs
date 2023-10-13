// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRestException.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Models.Rest;

namespace DiscordBotApi.Rest;

public class DiscordRestException : ApplicationException
{
	public DiscordRestException(HttpStatusCode statusCode, DiscordErrorResponse errorResponse) : base(
		message: CreateMessage(errorResponse: errorResponse))
	{
		StatusCode = statusCode;
		ErrorResponse = errorResponse;
	}

	public DiscordErrorResponse ErrorResponse { get; set; }

	public HttpStatusCode StatusCode { get; set; }

	private static string CreateMessage(DiscordErrorResponse errorResponse) =>
		$"{(int)errorResponse.Code} ({errorResponse.Code}): {errorResponse.Message}";
}