// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Utilities;

// https://discord.com/developers/docs/reference#message-formatting
public static class MentionUtils
{
	public static string MentionChannel(ulong channelId) => $"<#{channelId}>";

	public static string MentionRole(ulong roleId) => $"<@&{roleId}>";

	public static string MentionUser(ulong userId) => $"<@{userId}>";
}