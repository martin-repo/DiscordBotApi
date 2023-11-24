// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumMessageArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordForumMessageArgsBuilder
{
	private List<DiscordMessageAttachment>? _attachments;
	private List<DiscordMessageActionRow>? _components;
	private string? _content;
	private List<DiscordEmbed>? _embeds;
	private List<DiscordMessageFile>? _files;

	public DiscordForumMessageArgsBuilder AddAttachment(Action<DiscordMessageAttachmentBuilder> builderAction)
	{
		var builder = new DiscordMessageAttachmentBuilder();
		builderAction(obj: builder);
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: builder.Build());
		return this;
	}

	public DiscordForumMessageArgsBuilder AddAttachment(DiscordMessageAttachment item)
	{
		_attachments ??= new List<DiscordMessageAttachment>();
		_attachments.Add(item: item);
		return this;
	}

	public DiscordForumMessageArgsBuilder AddComponent(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components ??= new List<DiscordMessageActionRow>();
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordForumMessageArgsBuilder AddComponent(DiscordMessageActionRow item)
	{
		_components ??= new List<DiscordMessageActionRow>();
		_components.Add(item: item);
		return this;
	}

	public DiscordForumMessageArgsBuilder WithContent(string? content)
	{
		_content = content;
		return this;
	}

	public DiscordForumMessageArgsBuilder AddEmbed(Action<DiscordEmbedBuilder> builderAction)
	{
		var builder = new DiscordEmbedBuilder();
		builderAction(obj: builder);
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: builder.Build());
		return this;
	}

	public DiscordForumMessageArgsBuilder AddEmbed(DiscordEmbed item)
	{
		_embeds ??= new List<DiscordEmbed>();
		_embeds.Add(item: item);
		return this;
	}

	public DiscordForumMessageArgsBuilder AddFile(Action<DiscordMessageFileBuilder> builderAction)
	{
		var builder = new DiscordMessageFileBuilder();
		builderAction(obj: builder);
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: builder.Build());
		return this;
	}

	public DiscordForumMessageArgsBuilder AddFile(DiscordMessageFile item)
	{
		_files ??= new List<DiscordMessageFile>();
		_files.Add(item: item);
		return this;
	}

	public DiscordForumMessageArgs Build() =>
		new()
		{
			Attachments = _attachments?.ToImmutableArray(),
			Components = _components?.ToImmutableArray(),
			Content = _content,
			Embeds = _embeds?.ToImmutableArray(),
			Files = _files?.ToImmutableArray(),
		};
}