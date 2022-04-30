// -------------------------------------------------------------------------------------------------
// <copyright file="EmojiUtils.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public static class EmojiUtils
    {
        // https://discord.com/developers/docs/reference#image-formatting
        private const string ImageBaseUrl = "https://cdn.discordapp.com/";

        public static string ToEmojiLink(DiscordEmoji emoji)
        {
            return $"<:{emoji.Name}:{emoji.Id}>";
        }

        public static string ToEmojiUrl(DiscordEmoji emoji)
        {
            var extension = emoji.Animated!.Value ? "gif" : "png";
            return $"{ImageBaseUrl}emojis/{emoji.Id}.{extension}";
        }
    }
}