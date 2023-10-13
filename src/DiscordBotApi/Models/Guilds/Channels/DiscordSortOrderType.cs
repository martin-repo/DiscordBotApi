// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-sort-order-types
public enum DiscordSortOrderType
{
	LatestActivity = 0,
	CreationDate = 1
}