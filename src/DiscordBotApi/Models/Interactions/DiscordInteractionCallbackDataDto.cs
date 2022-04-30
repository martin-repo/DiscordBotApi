// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackDataDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(DiscordInteractionCallbackDataDtoConverter))]
    internal abstract record DiscordInteractionCallbackDataDto;
}