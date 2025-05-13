// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSortOrderType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-sort-order-types
public enum DiscordSortOrderType
{
	LatestActivity = 0,
	CreationDate = 1
}