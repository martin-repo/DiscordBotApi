// -------------------------------------------------------------------------------------------------
// <copyright file="EmojiUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Utilities;

public static class EmojiUtils
{
	// https://discord.com/developers/docs/reference#image-formatting
	private const string ImageBaseUrl = "https://cdn.discordapp.com/";

	// Type "\:emoji_name:" in Discord to get name and id of custom emoji
	public static string ToEmojiLink(DiscordEmoji emoji) => $"<:{emoji.Name}:{emoji.Id}>";

	public static string ToEmojiUrl(DiscordEmoji emoji)
	{
		var extension = emoji.Animated == true ? "gif" : "png";
		return $"{ImageBaseUrl}emojis/{emoji.Id}.{extension}";
	}
}