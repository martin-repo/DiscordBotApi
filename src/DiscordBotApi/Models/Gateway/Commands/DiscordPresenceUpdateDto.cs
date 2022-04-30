// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdateDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Utilities;

    internal record DiscordPresenceUpdateDto(
        [property: JsonPropertyName("since")] int? Since,
        [property: JsonPropertyName("activities")] DiscordActivityUpdateDto[] Activities,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("afk")] bool Afk)
    {
        internal DiscordPresenceUpdateDto(DiscordPresenceUpdate model)
            : this(
                model.Since != null ? DateTimeUtils.ToEpochTimeMilliseconds(model.Since.Value) : null,
                model.Activities.Select(a => new DiscordActivityUpdateDto(a)).ToArray(),
                GetStatusString(model.Status),
                model.Afk)
        {
        }

        private static string GetStatusString(DiscordPresenceStatus status)
        {
            switch (status)
            {
                case DiscordPresenceStatus.Online:
                    return "online";
                case DiscordPresenceStatus.DoNotDisturb:
                    return "dnd";
                case DiscordPresenceStatus.Idle:
                    return "idle";
                case DiscordPresenceStatus.Invisible:
                    return "invisible";
                case DiscordPresenceStatus.Offline:
                    return "offline";
                default:
                    throw new NotSupportedException($"{nameof(DiscordPresenceStatus)} {status} is not supported");
            }
        }
    }
}