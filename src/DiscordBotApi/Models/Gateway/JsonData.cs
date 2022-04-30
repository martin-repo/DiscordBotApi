// -------------------------------------------------------------------------------------------------
// <copyright file="JsonData.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(JsonDataConverter))]
    internal record JsonData(string Json);
}