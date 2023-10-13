// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResourceId.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal record DiscordResourceId(string HttpMethod, string Path)
{
	public override string ToString() => $"{HttpMethod}:{Path}";
}