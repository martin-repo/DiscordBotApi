// -------------------------------------------------------------------------------------------------
// <copyright file="JsonData.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

[JsonConverter(converterType: typeof(JsonDataConverter))]
internal record JsonData(string Json);