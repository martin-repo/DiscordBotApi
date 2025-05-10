// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-channel-types
public enum DiscordChannelType
{
	GuildText = 0,
	Dm = 1,
	GuildVoice = 2,
	GroupDm = 3,
	GuildCategory = 4,
	GuildAnnouncement = 5,
	AnnouncementThread = 10,
	PublicThread = 11,
	PrivateThread = 12,
	GuildStageVoice = 13,
	GuildDirectory = 14,
	GuildForum = 15
}