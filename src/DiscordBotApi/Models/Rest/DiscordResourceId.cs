// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResourceId.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal sealed record DiscordResourceId(string HttpMethod, string Path)
{
	public override string ToString() => $"{HttpMethod}:{Path}";
}