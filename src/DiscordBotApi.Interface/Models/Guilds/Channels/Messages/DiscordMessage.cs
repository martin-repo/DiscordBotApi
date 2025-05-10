// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessage.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordMessage : DiscordMessageBase
{
	public required IReadOnlyCollection<DiscordMessageAttachment> Attachments { get; init; }

	public required DiscordUser Author { get; init; }

	public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

	/// <remarks>
	/// Requires privileged gateway intents. See bot page for more information.
	/// </remarks>
	public required string Content { get; init; }

	public DateTime? EditedTimestamp { get; init; }

	public required IReadOnlyCollection<DiscordEmbed> Embeds { get; init; }

	public DiscordMessageFlags? Flags { get; init; }

	public ulong? GuildId { get; init; }

	public DiscordGuildMember? Member { get; init; }

	public required ImmutableArray<DiscordUser> Mentions { get; init; }

	public DiscordMessageReference? MessageReference { get; init; }

	public string? Nonce { get; init; }

	public required bool Pinned { get; init; }

	public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

	public DiscordMessage? ReferencedMessage { get; init; }

	public DiscordChannel? Thread { get; init; }

	public required DateTime Timestamp { get; init; }

	public required DiscordMessageType Type { get; init; }
}