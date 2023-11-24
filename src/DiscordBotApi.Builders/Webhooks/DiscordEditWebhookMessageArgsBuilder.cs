// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEditWebhookMessageArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Webhooks;

public class DiscordEditWebhookMessageArgsBuilder
{
	private List<DiscordMessageAttachment>? _attachments;
	private List<DiscordMessageComponent>? _components;
	private string? _content;
	private List<DiscordEmbed>? _embeds;
	private List<DiscordMessageFile>? _files;

	public DiscordEditWebhookMessageArgsBuilder AddAttachment(Action<DiscordMessageAttachmentBuilder> builderAction)
	{
		var builder = new DiscordMessageAttachmentBuilder();
		builderAction(obj: builder);
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: builder.Build());
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddAttachment(DiscordMessageAttachment item)
	{
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: item);
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components ??= new List<DiscordMessageComponent>();
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder WithContent(string? content)
	{
		_content = content;
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddEmbed(Action<DiscordEmbedBuilder> builderAction)
	{
		var builder = new DiscordEmbedBuilder();
		builderAction(obj: builder);
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: builder.Build());
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddEmbed(DiscordEmbed item)
	{
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: item);
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddFile(Action<DiscordMessageFileBuilder> builderAction)
	{
		var builder = new DiscordMessageFileBuilder();
		builderAction(obj: builder);
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: builder.Build());
		return this;
	}

	public DiscordEditWebhookMessageArgsBuilder AddFile(DiscordMessageFile item)
	{
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: item);
		return this;
	}

	public DiscordEditWebhookMessageArgs Build() =>
		new()
		{
			Attachments = _attachments?.ToImmutableArray(),
			Components = _components?.ToImmutableArray(),
			Content = _content,
			Embeds = _embeds?.ToImmutableArray(),
			Files = _files?.ToImmutableArray(),
		};
}