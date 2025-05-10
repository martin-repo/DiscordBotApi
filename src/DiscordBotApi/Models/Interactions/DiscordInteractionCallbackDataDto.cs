// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackDataDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Interactions;

[JsonConverter(converterType: typeof(DiscordInteractionCallbackDataDtoConverter))]
internal abstract class DiscordInteractionCallbackDataDto;