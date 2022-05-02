// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientChannel.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using System.Net;

    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Guilds.Emojis;
    using DiscordBotApi.Rest;

    using Microsoft.AspNetCore.StaticFiles;

    public partial class DiscordBotClient
    {
        public async Task BulkDeleteMessagesAsync(ulong channelId, DiscordBulkDeleteMessagesArgs args, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/bulk-delete";

            var argsDto = new DiscordBulkDeleteMessagesArgsDto(args);
            await _restClient.SendRequestAsync(
                                 () =>
                                 {
                                     var request = new HttpRequestMessage(HttpMethod.Post, url);
                                     request.Content = _restClient.CreateJsonContent(argsDto);
                                     return request;
                                 },
                                 HttpStatusCode.NoContent,
                                 cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task<DiscordMessage> CreateMessageAsync(
            ulong channelId,
            DiscordCreateMessageArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages";

            var argsDto = new DiscordCreateMessageArgsDto(args);
            var payloadJson = _restClient.CreateJsonContent(argsDto);

            using var content = CreateContentForMessage(args.Attachments, args.Files, payloadJson);

            // ReSharper disable once AccessToDisposedClosure
            var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
                                                  () => new HttpRequestMessage(HttpMethod.Post, url) { Content = content },
                                                  cancellationToken)
                                              .ConfigureAwait(false);

            var message = new DiscordMessage(this, messageDto);
            return message;
        }

        public async Task CreateReactionAsync(
            ulong channelId,
            ulong messageId,
            DiscordEmoji emoji,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}/reactions/{GetEncodedEmojiValue(emoji)}/@me";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Put, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task DeleteAllReactionsForEmojiAsync(
            ulong channelId,
            ulong messageId,
            DiscordEmoji emoji,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}/reactions/{GetEncodedEmojiValue(emoji)}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task DeleteMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task DeleteOrCloseChannelAsync(ulong channelId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.OK, cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task<DiscordMessage> EditMessageAsync(
            ulong channelId,
            ulong messageId,
            DiscordEditMessageArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}";

            var argsDto = new DiscordEditMessageArgsDto(args);
            var payloadJson = _restClient.CreateJsonContent(argsDto);

            using var content = CreateContentForMessage(args.Attachments, args.Files, payloadJson);

            // ReSharper disable once AccessToDisposedClosure
            var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
                                                  () => new HttpRequestMessage(HttpMethod.Patch, url) { Content = content },
                                                  cancellationToken)
                                              .ConfigureAwait(false);

            var message = new DiscordMessage(this, messageDto);
            return message;
        }

        public async Task<DiscordChannel> GetChannelAsync(ulong channelId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}";

            var channelDto = await _restClient
                                   .SendRequestAsync<DiscordChannelDto>(() => new HttpRequestMessage(HttpMethod.Get, url), cancellationToken)
                                   .ConfigureAwait(false);

            var channel = new DiscordChannel(this, channelDto);
            return channel;
        }

        public async Task<DiscordMessage> GetChannelMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}";

            var messageDto = await _restClient
                                   .SendRequestAsync<DiscordMessageDto>(() => new HttpRequestMessage(HttpMethod.Get, url), cancellationToken)
                                   .ConfigureAwait(false);

            var message = new DiscordMessage(this, messageDto);
            return message;
        }

        public async Task<IReadOnlyCollection<DiscordMessage>> GetChannelMessagesAsync(
            ulong channelId,
            DiscordGetChannelMessagesArgs? args = null,
            CancellationToken cancellationToken = default)
        {
            var builder = new QueryBuilder($"channels/{channelId}/messages");
            builder.Add("around", args?.Around);
            builder.Add("before", args?.Before);
            builder.Add("after", args?.After);
            builder.Add("limit", args?.Limit);

            var messageDtos = await _restClient.SendRequestAsync<DiscordMessageDto[]>(
                                                   () => new HttpRequestMessage(HttpMethod.Get, builder.ToString()),
                                                   cancellationToken)
                                               .ConfigureAwait(false);

            var messages = messageDtos.Select(m => new DiscordMessage(this, m)).ToArray();
            return messages;
        }

        public async Task<IReadOnlyCollection<DiscordMessage>> GetPinnedMessagesAsync(ulong channelId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/pins";

            var messageDtos = await _restClient
                                    .SendRequestAsync<DiscordMessageDto[]>(() => new HttpRequestMessage(HttpMethod.Get, url), cancellationToken)
                                    .ConfigureAwait(false);

            var messages = messageDtos.Select(m => new DiscordMessage(this, m)).ToArray();
            return messages;
        }

        public async Task<DiscordThreadResponse> ListPublicArchivedThreadsAsync(
            ulong channelId,
            DiscordListPublicArchivedThreadsArgs? args = null,
            CancellationToken cancellationToken = default)
        {
            var builder = new QueryBuilder($"channels/{channelId}/threads/archived/public");
            builder.Add("before", args?.Before?.ToString("O"));
            builder.Add("limit", args?.Limit);

            var responseDto = await _restClient.SendRequestAsync<DiscordThreadResponseDto>(
                                                   () => new HttpRequestMessage(HttpMethod.Get, builder.ToString()),
                                                   cancellationToken)
                                               .ConfigureAwait(false);

            var response = new DiscordThreadResponse(this, responseDto);
            return response;
        }

        public async Task<DiscordChannel> ModifyChannelAsync(
            ulong channelId,
            DiscordModifyGuildChannelArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}";

            var argsDto = new DiscordModifyGuildChannelArgsDto(args);
            var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
                                                  () =>
                                                  {
                                                      var request = new HttpRequestMessage(HttpMethod.Patch, url);
                                                      request.Content = _restClient.CreateJsonContent(argsDto);
                                                      return request;
                                                  },
                                                  cancellationToken)
                                              .ConfigureAwait(false);

            var channel = new DiscordChannel(this, channelDto);
            return channel;
        }

        public async Task<DiscordChannel> ModifyThreadAsync(
            ulong channelId,
            DiscordModifyThreadArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}";

            var argsDto = new DiscordModifyThreadArgsDto(args);
            var modifiedChannelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
                                                          () =>
                                                          {
                                                              var request = new HttpRequestMessage(HttpMethod.Patch, url);
                                                              request.Content = _restClient.CreateJsonContent(argsDto);
                                                              return request;
                                                          },
                                                          cancellationToken)
                                                      .ConfigureAwait(false);

            var modifiedChannel = new DiscordChannel(this, modifiedChannelDto);
            return modifiedChannel;
        }

        public async Task PinMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/pins/{messageId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Put, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task<DiscordChannel> StartThreadWithMessageAsync(
            ulong channelId,
            ulong messageId,
            DiscordStartThreadWithMessageArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/messages/{messageId}/threads";

            var argsDto = new DiscordStartThreadWithMessageArgsDto(args);
            var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
                                                  () =>
                                                  {
                                                      var request = new HttpRequestMessage(HttpMethod.Post, url);
                                                      request.Content = _restClient.CreateJsonContent(argsDto);
                                                      return request;
                                                  },
                                                  cancellationToken)
                                              .ConfigureAwait(false);

            var channel = new DiscordChannel(this, channelDto);
            return channel;
        }

        public async Task UnpinMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default)
        {
            var url = $"channels/{channelId}/pins/{messageId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        private static string GetContentType(string fileName)
        {
            var success = new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
            return success ? contentType : "application/octet-stream";
        }

        private static string GetEncodedEmojiValue(DiscordEmoji emoji)
        {
            if (emoji.Name == null)
            {
                throw new ArgumentException("Emoji name cannot be null.");
            }

            var emojiValue = emoji.Id != null ? $"{emoji.Name}:{emoji.Id}" : emoji.Name;
            var encodedEmojiValue = WebUtility.UrlEncode(emojiValue);

            return encodedEmojiValue;
        }

        private HttpContent CreateContentForMessage(
            IReadOnlyCollection<DiscordMessageAttachment>? attachments,
            IReadOnlyCollection<DiscordMessageFile>? files,
            HttpContent payloadJson)
        {
            if (files == null || !files.Any())
            {
                return payloadJson;
            }

            if (attachments == null)
            {
                throw new ArgumentException("Attachments are required when Files are defined.");
            }

            var dataContent = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var attachment = attachments.FirstOrDefault(a => a.Id == file.Id);
                if (attachment == null)
                {
                    throw new ArgumentException($"File with id {file.Id} does not have a corresponding Attachment.");
                }

                if (attachment.Filename == null)
                {
                    throw new ArgumentException("Attachment.FileName is required when a file is being sent.");
                }

                var fileStream = File.OpenRead(file.FilePath);
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.Add("Content-Type", GetContentType(attachment.Filename));
                dataContent.Add(streamContent, $"files[{file.Id}]", attachment.Filename);
            }

            dataContent.Add(payloadJson, "payload_json");

            return dataContent;
        }
    }
}