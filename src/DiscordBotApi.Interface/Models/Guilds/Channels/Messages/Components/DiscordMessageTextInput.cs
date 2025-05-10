// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageTextInput.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

public sealed class DiscordMessageTextInput : DiscordMessageComponent
{
	public required string CustomId { get; init; }

	public required string Label { get; init; }

	public int? MaxLength { get; init; }

	public int? MinLength { get; init; }

	public string? Placeholder { get; init; }

	public bool? Required { get; init; }

	public required DiscordMessageTextInputStyle Style { get; init; }

	public string? Value { get; init; }
}