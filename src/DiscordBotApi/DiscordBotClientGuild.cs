// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientGuild.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using System.Net;
    using System.Text;

    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Models.Guilds.Emojis;
    using DiscordBotApi.Rest;

    public partial class DiscordBotClient
    {
        public async Task AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
        {
            var url = $"guilds/{guildId}/members/{userId}/roles/{roleId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Put, url), HttpStatusCode.NoContent).ConfigureAwait(false);
        }

        public async Task<DiscordChannel> CreateGuildChannelAsync(ulong guildId, DiscordCreateGuildChannelArgs args)
        {
            var url = $"guilds/{guildId}/channels";

            var argsDto = new DiscordCreateGuildChannelArgsDto(args);
            var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
                                                  () =>
                                                  {
                                                      var request = new HttpRequestMessage(HttpMethod.Post, url);
                                                      request.Content = _restClient.CreateJsonContent(argsDto);
                                                      return request;
                                                  })
                                              .ConfigureAwait(false);

            var channel = new DiscordChannel(this, channelDto);
            return channel;
        }

        // https://discord.com/developers/docs/resources/emoji#create-guild-emoji
        public async Task<DiscordEmoji> CreateGuildEmojiAsync(ulong guildId, DiscordCreateGuildEmojiArgs args)
        {
            var url = $"guilds/{guildId}/emojis";

            var imageBuilder = new StringBuilder();
            imageBuilder.Append("data:");
            switch (Path.GetExtension(args.FilePath).ToLowerInvariant().Trim('.'))
            {
                case "gif":
                    imageBuilder.Append("image/gif");
                    break;
                case "png":
                    imageBuilder.Append("image/png");
                    break;
                default:
                    throw new ArgumentException("Invalid file type.");
            }

            imageBuilder.Append(";base64,");
            imageBuilder.Append(Convert.ToBase64String(await File.ReadAllBytesAsync(args.FilePath).ConfigureAwait(false)));

            var argsDto = new DiscordCreateGuildEmojiArgsDto(args.Name, imageBuilder.ToString());
            var emojiDto = await _restClient.SendRequestAsync<DiscordEmojiDto>(
                                                () =>
                                                {
                                                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                                                    request.Content = _restClient.CreateJsonContent(argsDto);
                                                    return request;
                                                })
                                            .ConfigureAwait(false);

            var emoji = new DiscordEmoji(emojiDto);
            return emoji;
        }

        public async Task<DiscordRole> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRoleArgs args)
        {
            var url = $"guilds/{guildId}/roles";

            var argsDto = new DiscordCreateGuildRoleArgsDto(args);
            var roleDto = await _restClient.SendRequestAsync<DiscordRoleDto>(
                                               () =>
                                               {
                                                   var request = new HttpRequestMessage(HttpMethod.Post, url);
                                                   request.Content = _restClient.CreateJsonContent(argsDto);
                                                   return request;
                                               })
                                           .ConfigureAwait(false);

            var role = new DiscordRole(this, guildId, roleDto);
            return role;
        }

        public async Task DeleteGuildEmojiAsync(ulong guildId, ulong emojiId)
        {
            var url = $"guilds/{guildId}/emojis/{emojiId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent).ConfigureAwait(false);
        }

        public async Task DeleteGuildRoleAsync(ulong guildId, ulong roleId)
        {
            var url = $"guilds/{guildId}/roles/{roleId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent).ConfigureAwait(false);
        }

        public async Task<DiscordGuild> GetGuildAsync(ulong guildId, DiscordGetGuildArgs? args = null)
        {
            var builder = new QueryBuilder($"guilds/{guildId}");
            builder.Add("with_counts", args?.WithCounts);

            var guildDto = await _restClient.SendRequestAsync<DiscordGuildDto>(() => new HttpRequestMessage(HttpMethod.Get, builder.ToString()))
                                            .ConfigureAwait(false);

            var guild = new DiscordGuild(this, guildDto);

            return guild;
        }

        public async Task<IReadOnlyCollection<DiscordChannel>> GetGuildChannelsAsync(ulong guildId)
        {
            var url = $"guilds/{guildId}/channels";

            var channelDtos = await _restClient.SendRequestAsync<DiscordChannelDto[]>(() => new HttpRequestMessage(HttpMethod.Get, url))
                                               .ConfigureAwait(false);

            var channels = channelDtos.Select(c => new DiscordChannel(this, c)).ToArray();

            return channels;
        }

        public async Task<DiscordGuildMember> GetGuildMemberAsync(ulong guildId, ulong userId)
        {
            var url = $"guilds/{guildId}/members/{userId}";

            var memberDto = await _restClient.SendRequestAsync<DiscordGuildMemberDto>(() => new HttpRequestMessage(HttpMethod.Get, url))
                                             .ConfigureAwait(false);

            var member = new DiscordGuildMember(memberDto);
            return member;
        }

        public async Task<IReadOnlyCollection<DiscordRole>> GetGuildRolesAsync(ulong guildId)
        {
            var url = $"guilds/{guildId}/roles";

            var roleDtos = await _restClient.SendRequestAsync<DiscordRoleDto[]>(() => new HttpRequestMessage(HttpMethod.Get, url))
                                            .ConfigureAwait(false);

            var roles = roleDtos.Select(r => new DiscordRole(this, guildId, r)).ToArray();
            return roles;
        }

        public async Task<IReadOnlyCollection<DiscordEmoji>> ListGuildEmojisAsync(ulong guildId)
        {
            var url = $"guilds/{guildId}/emojis";

            var emojiDtos = await _restClient.SendRequestAsync<DiscordEmojiDto[]>(() => new HttpRequestMessage(HttpMethod.Get, url))
                                             .ConfigureAwait(false);

            var emojis = emojiDtos.Select(e => new DiscordEmoji(e)).ToArray();
            return emojis;
        }

        public async Task<DiscordRole> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRoleArgs args)
        {
            var url = $"guilds/{guildId}/roles/{roleId}";

            var argsDto = new DiscordModifyGuildRoleArgsDto(args);
            var roleDto = await _restClient.SendRequestAsync<DiscordRoleDto>(
                                               () =>
                                               {
                                                   var request = new HttpRequestMessage(HttpMethod.Patch, url);
                                                   request.Content = _restClient.CreateJsonContent(argsDto);
                                                   return request;
                                               })
                                           .ConfigureAwait(false);

            var role = new DiscordRole(this, guildId, roleDto);
            return role;
        }

        public async Task RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId)
        {
            var url = $"guilds/{guildId}/members/{userId}/roles/{roleId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent).ConfigureAwait(false);
        }
    }
}