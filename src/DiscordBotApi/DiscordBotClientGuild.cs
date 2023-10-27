// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientGuild.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Net;
using System.Text;

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Emojis;
using DiscordBotApi.Rest;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	public async Task AddGuildMemberRoleAsync(
		ulong guildId,
		ulong userId,
		ulong roleId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/members/{userId}/roles/{roleId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Put, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task<DiscordChannel> CreateGuildChannelAsync(
		ulong guildId,
		DiscordCreateGuildChannelArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/channels";

		var argsDto = new DiscordCreateGuildChannelArgsDto(model: args);
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

	// https://discord.com/developers/docs/resources/emoji#create-guild-emoji
	public async Task<DiscordEmoji> CreateGuildEmojiAsync(
		ulong guildId,
		DiscordCreateGuildEmojiArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/emojis";

		var imageBuilder = new StringBuilder();
		imageBuilder.Append(value: "data:");
		switch (Path.GetExtension(path: args.FilePath)
			.ToLowerInvariant()
			.Trim(trimChar: '.'))
		{
			case "gif":
				imageBuilder.Append(value: "image/gif");
				break;
			case "png":
				imageBuilder.Append(value: "image/png");
				break;
			default:
				throw new ArgumentException(message: "Invalid file type.");
		}

		imageBuilder.Append(value: ";base64,");
		imageBuilder.Append(
			value: Convert.ToBase64String(
				inArray: await File.ReadAllBytesAsync(path: args.FilePath, cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false)));

		var argsDto = new DiscordCreateGuildEmojiArgsDto(Name: args.Name, Image: imageBuilder.ToString());
		var emojiDto = await _restClient.SendRequestAsync<DiscordEmojiDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var emoji = new DiscordEmoji(dto: emojiDto);
		return emoji;
	}

	public async Task<DiscordRole> CreateGuildRoleAsync(
		ulong guildId,
		DiscordCreateGuildRoleArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/roles";

		var argsDto = new DiscordCreateGuildRoleArgsDto(model: args);
		var roleDto = await _restClient.SendRequestAsync<DiscordRoleDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var role = new DiscordRole(botClient: this, guildId: guildId, dto: roleDto);
		return role;
	}

	public async Task DeleteGuildEmojiAsync(ulong guildId, ulong emojiId, CancellationToken cancellationToken = default)
	{
		var url = $"guilds/{guildId}/emojis/{emojiId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DeleteGuildRoleAsync(ulong guildId, ulong roleId, CancellationToken cancellationToken = default)
	{
		var url = $"guilds/{guildId}/roles/{roleId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task<DiscordGuild> GetGuildAsync(
		ulong guildId,
		DiscordGetGuildArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"guilds/{guildId}");
		builder.Add(key: "with_counts", value: args?.WithCounts);

		var guildDto = await _restClient.SendRequestAsync<DiscordGuildDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var guild = new DiscordGuild(botClient: this, dto: guildDto);

		return guild;
	}

	public async Task<IReadOnlyCollection<DiscordChannel>> GetGuildChannelsAsync(
		ulong guildId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/channels";

		var channelDtos = await _restClient.SendRequestAsync<DiscordChannelDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channels = channelDtos.Select(selector: c => new DiscordChannel(botClient: this, dto: c))
			.ToArray();

		return channels;
	}

	public async Task<DiscordGuildMember> GetGuildMemberAsync(
		ulong guildId,
		ulong userId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/members/{userId}";

		var memberDto = await _restClient.SendRequestAsync<DiscordGuildMemberDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var member = new DiscordGuildMember(dto: memberDto);
		return member;
	}

	public async Task<IReadOnlyCollection<DiscordRole>> GetGuildRolesAsync(
		ulong guildId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/roles";

		var roleDtos = await _restClient.SendRequestAsync<DiscordRoleDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var roles = roleDtos.Select(selector: r => new DiscordRole(botClient: this, guildId: guildId, dto: r))
			.ToArray();
		return roles;
	}

	public async Task<DiscordActiveThreadResponse> ListActiveGuildThreadsAsync(
		ulong guildId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/threads/active";

		var responseDto = await _restClient.SendRequestAsync<DiscordActiveThreadResponseDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var response = new DiscordActiveThreadResponse(botClient: this, dto: responseDto);
		return response;
	}

	public async Task<IReadOnlyCollection<DiscordEmoji>> ListGuildEmojisAsync(
		ulong guildId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/emojis";

		var emojiDtos = await _restClient.SendRequestAsync<DiscordEmojiDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var emojis = emojiDtos.Select(selector: e => new DiscordEmoji(dto: e))
			.ToArray();
		return emojis;
	}

	// https://discord.com/developers/docs/resources/guild#list-guild-members
	public async Task<ImmutableArray<DiscordGuildMember>> ListGuildMembersAsync(
		ulong guildId,
		DiscordListGuildMembersArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"guilds/{guildId}/members");
		builder.Add(key: "limit", value: args?.Limit);
		builder.Add(key: "after", value: args?.After);

		var guildMemberDtos = await _restClient.SendRequestAsync<DiscordGuildMemberDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var guildMembers = guildMemberDtos.Select(selector: e => new DiscordGuildMember(dto: e))
			.ToImmutableArray();
		return guildMembers;
	}

	public async Task<DiscordRole> ModifyGuildRoleAsync(
		ulong guildId,
		ulong roleId,
		DiscordModifyGuildRoleArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/roles/{roleId}";

		var argsDto = new DiscordModifyGuildRoleArgsDto(model: args);
		var roleDto = await _restClient.SendRequestAsync<DiscordRoleDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var role = new DiscordRole(botClient: this, guildId: guildId, dto: roleDto);
		return role;
	}

	public async Task RemoveGuildMemberRoleAsync(
		ulong guildId,
		ulong userId,
		ulong roleId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"guilds/{guildId}/members/{userId}/roles/{roleId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}
}