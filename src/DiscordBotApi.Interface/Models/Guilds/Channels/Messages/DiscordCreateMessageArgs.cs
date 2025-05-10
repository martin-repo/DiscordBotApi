// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordCreateMessageArgs
{
	public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

	public IReadOnlyCollection<DiscordMessageComponent>? Components { get; init; }

	public string? Content { get; init; }

	public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

	public IReadOnlyCollection<DiscordMessageFile>? Files { get; init; }

	public DiscordMessageFlags? Flags { get; init; }

	public DiscordMessageReference? MessageReference { get; init; }

	public string? Nonce { get; init; }
}