// -------------------------------------------------------------------------------------------------
// <copyright file="EmojiUtils.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Utilities;

public static class EmojiUtils
{
	// https://discord.com/developers/docs/reference#image-formatting
	private const string ImageBaseUrl = "https://cdn.discordapp.com/";

	public static string ToEmojiLink(DiscordEmoji emoji) => $"<:{emoji.Name}:{emoji.Id}>";

	public static string ToEmojiUrl(DiscordEmoji emoji)
	{
		var extension = emoji.Animated!.Value
			? "gif"
			: "png";
		return $"{ImageBaseUrl}emojis/{emoji.Id}.{extension}";
	}
}