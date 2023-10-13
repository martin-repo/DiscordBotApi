// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumLayoutType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-forum-layout-types
public enum DiscordForumLayoutType
{
	NotSet = 0,
	ListView = 1,
	GalleryView = 2
}