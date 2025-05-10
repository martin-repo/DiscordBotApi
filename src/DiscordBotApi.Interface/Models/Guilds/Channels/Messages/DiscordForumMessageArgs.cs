// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumMessageArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordForumMessageArgs
{
	public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

	public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

	public string? Content { get; init; }

	public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

	public IReadOnlyCollection<DiscordMessageFile>? Files { get; init; }
}