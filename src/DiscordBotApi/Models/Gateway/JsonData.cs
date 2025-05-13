// -------------------------------------------------------------------------------------------------
// <copyright file="JsonData.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

[JsonConverter(converterType: typeof(JsonDataConverter))]
internal sealed record JsonData(string Json);