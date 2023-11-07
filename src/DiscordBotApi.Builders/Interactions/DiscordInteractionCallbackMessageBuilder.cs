// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackMessageBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Interactions;

public class DiscordInteractionCallbackMessageBuilder
{
	private List<DiscordMessageAttachment>? _attachments;
	private List<DiscordMessageComponent>? _components;
	private string? _content;
	private List<DiscordEmbed>? _embeds;
	private List<DiscordMessageFile>? _files;
	private DiscordMessageFlags? _flags;

	public DiscordInteractionCallbackMessageBuilder AddAttachment(Action<DiscordMessageAttachmentBuilder> builderAction)
	{
		var builder = new DiscordMessageAttachmentBuilder();
		builderAction(obj: builder);
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackMessageBuilder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components ??= new List<DiscordMessageComponent>();
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackMessageBuilder WithContent(string? content)
	{
		_content = content;
		return this;
	}

	public DiscordInteractionCallbackMessageBuilder AddEmbed(Action<DiscordEmbedBuilder> builderAction)
	{
		var builder = new DiscordEmbedBuilder();
		builderAction(obj: builder);
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackMessageBuilder AddFile(Action<DiscordMessageFileBuilder> builderAction)
	{
		var builder = new DiscordMessageFileBuilder();
		builderAction(obj: builder);
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackMessageBuilder WithFlags(DiscordMessageFlags? flags)
	{
		_flags = flags;
		return this;
	}

	public DiscordInteractionCallbackMessage Build() =>
		new()
		{
			Attachments = _attachments?.ToImmutableArray(),
			Components = _components?.ToImmutableArray(),
			Content = _content,
			Embeds = _embeds?.ToImmutableArray(),
			Files = _files?.ToImmutableArray(),
			Flags = _flags,
		};
}