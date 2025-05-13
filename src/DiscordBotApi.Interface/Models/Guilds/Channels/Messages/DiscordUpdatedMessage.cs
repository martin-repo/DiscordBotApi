// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessage.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordUpdatedMessage : DiscordMessageBase
{
	public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

	public DiscordUser? Author { get; init; }

	public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

	public string? Content { get; init; }

	public DateTime? EditedTimestamp { get; init; }

	public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

	public ulong? GuildId { get; init; }

	public bool? Pinned { get; init; }

	public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

	public DiscordChannel? Thread { get; init; }

	public DateTime? Timestamp { get; init; }
}