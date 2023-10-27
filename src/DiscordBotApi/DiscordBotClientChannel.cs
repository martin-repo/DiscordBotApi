// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientChannel.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;

using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Emojis;
using DiscordBotApi.Rest;

using Serilog;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	public async Task BulkDeleteMessagesAsync(
		ulong channelId,
		DiscordBulkDeleteMessagesArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/bulk-delete";

		var argsDto = new DiscordBulkDeleteMessagesArgsDto(model: args);
		await _restClient.SendRequestAsync(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task<(DiscordChannel Channel, DiscordMessage Message)> CreateForumThreadAsync(
		ulong channelId,
		DiscordCreateForumThreadArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/threads";

		var argsDto = new DiscordCreateForumThreadArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(
			attachments: args.Message.Attachments,
			files: args.Message.Files,
			payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var responseJson = await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channelDto = JsonSerializer.Deserialize<DiscordChannelDto>(json: responseJson) ??
			throw new SerializationException(message: "Failed to deserialize Json.");

		using var container = JsonDocument.Parse(json: responseJson) ??
			throw new SerializationException(message: "Failed to deserialize Json.");
		var messageJson = JsonSerializer.Serialize(value: container.RootElement.GetProperty(propertyName: "message"));
		var messageDto = JsonSerializer.Deserialize<DiscordMessageDto>(json: messageJson) ??
			throw new SerializationException(message: "Failed to deserialize Json.");

		var channel = new DiscordChannel(botClient: this, dto: channelDto);
		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return (channel, message);
	}

	public async Task<DiscordMessage> CreateMessageAsync(
		ulong channelId,
		DiscordCreateMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages";

		var argsDto = new DiscordCreateMessageArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	public async Task CreateReactionAsync(
		ulong channelId,
		ulong messageId,
		DiscordEmoji emoji,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/{messageId}/reactions/{GetEncodedEmojiValue(emoji: emoji)}/@me";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Put, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DeleteAllReactionsForEmojiAsync(
		ulong channelId,
		ulong messageId,
		DiscordEmoji emoji,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/{messageId}/reactions/{GetEncodedEmojiValue(emoji: emoji)}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DeleteMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
	{
		var url = $"channels/{channelId}/messages/{messageId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DeleteOrCloseChannelAsync(ulong channelId, CancellationToken cancellationToken = default)
	{
		var url = $"channels/{channelId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.OK,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task<DiscordMessage> EditMessageAsync(
		ulong channelId,
		ulong messageId,
		DiscordEditMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/{messageId}";

		var argsDto = new DiscordEditMessageArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	public async Task<DiscordChannel> GetChannelAsync(ulong channelId, CancellationToken cancellationToken = default)
	{
		var url = $"channels/{channelId}";

		var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channel = new DiscordChannel(botClient: this, dto: channelDto);
		return channel;
	}

	public async Task<DiscordMessage> GetChannelMessageAsync(
		ulong channelId,
		ulong messageId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/{messageId}";

		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	public async Task<IReadOnlyCollection<DiscordMessage>> GetChannelMessagesAsync(
		ulong channelId,
		DiscordGetChannelMessagesArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"channels/{channelId}/messages");
		builder.Add(key: "around", value: args?.Around);
		builder.Add(key: "before", value: args?.Before);
		builder.Add(key: "after", value: args?.After);
		builder.Add(key: "limit", value: args?.Limit);

		var messageDtos = await _restClient.SendRequestAsync<DiscordMessageDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var messages = messageDtos.Select(selector: m => new DiscordMessage(botClient: this, dto: m))
			.ToArray();
		return messages;
	}

	public async Task<IReadOnlyCollection<DiscordMessage>> GetPinnedMessagesAsync(
		ulong channelId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/pins";

		var messageDtos = await _restClient.SendRequestAsync<DiscordMessageDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var messages = messageDtos.Select(selector: m => new DiscordMessage(botClient: this, dto: m))
			.ToArray();
		return messages;
	}

	public async Task<DiscordThreadResponse> ListPublicArchivedThreadsAsync(
		ulong channelId,
		DiscordListPublicArchivedThreadsArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"channels/{channelId}/threads/archived/public");
		builder.Add(key: "before", value: args?.Before?.ToString(format: "O"));
		builder.Add(key: "limit", value: args?.Limit);

		var responseDto = await _restClient.SendRequestAsync<DiscordThreadResponseDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var response = new DiscordThreadResponse(botClient: this, dto: responseDto);
		return response;
	}

	public async Task<DiscordChannel> ModifyChannelAsync(
		ulong channelId,
		DiscordModifyGuildChannelArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}";

		var argsDto = new DiscordModifyGuildChannelArgsDto(model: args);
		var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channel = new DiscordChannel(botClient: this, dto: channelDto);
		return channel;
	}

	public async Task<DiscordChannel> ModifyThreadAsync(
		ulong channelId,
		DiscordModifyThreadArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}";

		var argsDto = new DiscordModifyThreadArgsDto(model: args);
		var modifiedChannelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var modifiedChannel = new DiscordChannel(botClient: this, dto: modifiedChannelDto);
		return modifiedChannel;
	}

	public async Task PinMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
	{
		var url = $"channels/{channelId}/pins/{messageId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Put, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task<DiscordChannel> StartThreadWithMessageAsync(
		ulong channelId,
		ulong messageId,
		DiscordStartThreadWithMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"channels/{channelId}/messages/{messageId}/threads";

		var argsDto = new DiscordStartThreadWithMessageArgsDto(model: args);
		var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channel = new DiscordChannel(botClient: this, dto: channelDto);
		return channel;
	}

	public async Task UnpinMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
	{
		var url = $"channels/{channelId}/pins/{messageId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private static string GetContentType(string fileName)
	{
		var success = TryGetContentType(fileName: fileName, contentType: out var contentType);
		return success
			? contentType!
			: "application/octet-stream";
	}

	private static string GetEncodedEmojiValue(DiscordEmoji emoji)
	{
		if (emoji.Name == null)
		{
			throw new ArgumentException(message: "Emoji name cannot be null.");
		}

		var emojiValue = emoji.Id != null
			? $"{emoji.Name}:{emoji.Id}"
			: emoji.Name;
		var encodedEmojiValue = WebUtility.UrlEncode(value: emojiValue);

		return encodedEmojiValue;
	}

	private static bool TryGetContentType(string fileName, [NotNullWhen(returnValue: true)] out string? contentType)
	{
		var extension = Path.GetExtension(path: fileName);
		if (string.IsNullOrWhiteSpace(value: extension))
		{
			contentType = null;
			return false;
		}

		// https://discord.com/developers/docs/reference#editing-message-attachments-using-attachments-within-embeds
		contentType = extension.ToLowerInvariant() switch
		{
			".gif" => "image/gif",
			".jpg" => "image/jpeg",
			".jpeg" => "image/jpeg",
			".png" => "image/png",
			".webp" => "image/webp",
			_ => null
		};

		if (contentType is not null)
		{
			return true;
		}

		Log.Warning(messageTemplate: "Unsupported file extension '{FileName}'", propertyValue: fileName);
		return false;
	}

	private HttpContent CreateContentForMessage(
		IReadOnlyCollection<DiscordMessageAttachment>? attachments,
		IReadOnlyCollection<DiscordMessageFile>? files,
		HttpContent payloadJson
	)
	{
		if (files == null ||
			!files.Any())
		{
			return payloadJson;
		}

		if (attachments == null)
		{
			throw new ArgumentException(message: "Attachments are required when Files are defined.");
		}

		var dataContent = new MultipartFormDataContent();

		foreach (var file in files)
		{
			var attachment = attachments.FirstOrDefault(predicate: a => a.Id == file.Id);
			if (attachment == null)
			{
				throw new ArgumentException(message: $"File with id {file.Id} does not have a corresponding Attachment.");
			}

			if (attachment.Filename == null)
			{
				throw new ArgumentException(message: "Attachment.FileName is required when a file is being sent.");
			}

			var fileStream = File.OpenRead(path: file.FilePath);
			var streamContent = new StreamContent(content: fileStream);
			streamContent.Headers.Add(name: "Content-Type", value: GetContentType(fileName: attachment.Filename));
			dataContent.Add(content: streamContent, name: $"files[{file.Id}]", fileName: attachment.Filename);
		}

		dataContent.Add(content: payloadJson, name: "payload_json");

		return dataContent;
	}
}