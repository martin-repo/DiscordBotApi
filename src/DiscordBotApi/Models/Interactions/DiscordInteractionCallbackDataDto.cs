// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackDataDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Interactions;

[JsonConverter(converterType: typeof(DiscordInteractionCallbackDataDtoConverter))]
internal abstract record DiscordInteractionCallbackDataDto;