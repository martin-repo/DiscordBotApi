// -------------------------------------------------------------------------------------------------
// <copyright file="JsonParseUtils.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities
{
    using System.Text.Json;

    using DiscordBotApi.Models.Applications;

    internal static class JsonParseUtils
    {
        public static object ToObject(DiscordApplicationCommandOptionType type, object jsonValue)
        {
            var jsonElement = (JsonElement)jsonValue;

            switch (type)
            {
                case DiscordApplicationCommandOptionType.String:
                    return jsonElement.ToString();

                case DiscordApplicationCommandOptionType.Integer:
                    return jsonElement.GetInt32();

                case DiscordApplicationCommandOptionType.Boolean:
                    return jsonElement.GetBoolean();

                case DiscordApplicationCommandOptionType.User:
                case DiscordApplicationCommandOptionType.Channel:
                case DiscordApplicationCommandOptionType.Role:
                case DiscordApplicationCommandOptionType.Mentionable:
                    return jsonElement.GetUInt64();

                case DiscordApplicationCommandOptionType.Number:
                    return jsonElement.GetDouble();

                default:
                    throw new NotSupportedException($"{nameof(DiscordApplicationCommandOptionType)} {type} is not supported");
            }
        }
    }
}