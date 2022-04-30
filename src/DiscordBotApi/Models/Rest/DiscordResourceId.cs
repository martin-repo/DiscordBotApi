// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResourceId.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    internal record DiscordResourceId(string HttpMethod, string Path)
    {
        public override string ToString()
        {
            return $"{HttpMethod}:{Path}";
        }
    }
}