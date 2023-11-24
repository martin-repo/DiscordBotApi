// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordCreateMessageArgsBuilder
{
	private List<DiscordMessageAttachment>? _attachments;
	private List<DiscordMessageComponent>? _components;
	private string? _content;
	private List<DiscordEmbed>? _embeds;
	private List<DiscordMessageFile>? _files;
	private DiscordMessageFlags? _flags;
	private DiscordMessageReference? _messageReference;
	private string? _nonce;

	public DiscordCreateMessageArgsBuilder AddAttachment(Action<DiscordMessageAttachmentBuilder> builderAction)
	{
		var builder = new DiscordMessageAttachmentBuilder();
		builderAction(obj: builder);
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddAttachment(DiscordMessageAttachment item)
	{
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: item);
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components ??= new List<DiscordMessageComponent>();
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateMessageArgsBuilder WithContent(string? content)
	{
		_content = content;
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddEmbed(Action<DiscordEmbedBuilder> builderAction)
	{
		var builder = new DiscordEmbedBuilder();
		builderAction(obj: builder);
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddEmbed(DiscordEmbed item)
	{
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: item);
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddFile(Action<DiscordMessageFileBuilder> builderAction)
	{
		var builder = new DiscordMessageFileBuilder();
		builderAction(obj: builder);
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateMessageArgsBuilder AddFile(DiscordMessageFile item)
	{
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: item);
		return this;
	}

	public DiscordCreateMessageArgsBuilder WithFlags(DiscordMessageFlags? flags)
	{
		_flags = flags;
		return this;
	}

	public DiscordCreateMessageArgsBuilder WithMessageReference(DiscordMessageReference? messageReference)
	{
		_messageReference = messageReference;
		return this;
	}

	public DiscordCreateMessageArgsBuilder WithNonce(string? nonce)
	{
		_nonce = nonce;
		return this;
	}

	public DiscordCreateMessageArgs Build() =>
		new()
		{
			Attachments = _attachments?.ToImmutableArray(),
			Components = _components?.ToImmutableArray(),
			Content = _content,
			Embeds = _embeds?.ToImmutableArray(),
			Files = _files?.ToImmutableArray(),
			Flags = _flags,
			MessageReference = _messageReference,
			Nonce = _nonce,
		};
}