// -------------------------------------------------------------------------------------------------
// <copyright file="JsonParseUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json;

using DiscordBotApi.Interface.Models.Applications;

namespace DiscordBotApi.Utilities;

internal static class JsonParseUtils
{
	public static object ToObject(DiscordApplicationCommandOptionType type, object jsonValue)
	{
		var jsonElement = (JsonElement)jsonValue;

		switch (type)
		{
			case DiscordApplicationCommandOptionType.String: return jsonElement.ToString();

			case DiscordApplicationCommandOptionType.Integer: return jsonElement.GetInt32();

			case DiscordApplicationCommandOptionType.Boolean: return jsonElement.GetBoolean();

			case DiscordApplicationCommandOptionType.User:
			case DiscordApplicationCommandOptionType.Channel:
			case DiscordApplicationCommandOptionType.Role:
			case DiscordApplicationCommandOptionType.Mentionable:
				return jsonElement.GetUInt64();

			case DiscordApplicationCommandOptionType.Number: return jsonElement.GetDouble();

			default:
				throw new NotSupportedException(
					message: $"{nameof(DiscordApplicationCommandOptionType)} {type} is not supported"
				);
		}
	}
}