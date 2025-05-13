// -------------------------------------------------------------------------------------------------
// <copyright file="RateLimitResourceManagerTests.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

using DiscordBotApi.Interface.Models.Rest;
using DiscordBotApi.Interface.Utilities;
using DiscordBotApi.Models.Rest;
using DiscordBotApi.Rest;
using DiscordBotApi.Utilities;

using FluentAssertions;

using Xunit;

namespace DiscordBotApi.Tests.Rest;

public class RateLimitResourceManagerTests
{
	[Fact]
	public async Task GetReservationAsync_ShouldWait_WhenBucketExistsAndIsEmpty()
	{
		var resourceManager = new DiscordResourceManager(logger: null);
		resourceManager.Start();

		var resource = new DiscordResourceId(HttpMethod: "GET", Path: "channels/123");
		var resetAfter = TimeSpan.FromMilliseconds(value: 100);
		var reset = DateTime.UtcNow.Add(value: resetAfter);
		var bucket = Guid.NewGuid()
			.ToString();

		using (var _ = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 0,
			cancellationToken: CancellationToken.None))
		{
		}

		resourceManager.UpdateResource(
			resourceId: resource,
			bucketResponse: new DiscordBucketResponse(
				bucket: bucket,
				limit: 1,
				remaining: 0,
				discordReset: 0,
				resetAfter: resetAfter),
			rateLimitResponse: null);

		using var reservation = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 1,
			cancellationToken: CancellationToken.None);

		DateTime.UtcNow.Should()
			.BeAfter(expected: reset);
	}

	[Fact]
	public async Task GetReservationAsync_ShouldWait_WhenBucketIsUpdated()
	{
		var resourceManager = new DiscordResourceManager(logger: null);
		resourceManager.Start();

		var resource = new DiscordResourceId(HttpMethod: "GET", Path: "channels/123");
		var resetAfter = TimeSpan.FromMilliseconds(value: 100);
		var reset = DateTime.UtcNow.Add(value: resetAfter);
		var discordReset = DateTimeUtils.ToEpochTimeSeconds(datetime: reset);
		var bucket = Guid.NewGuid()
			.ToString();

		using (var _ = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 0,
			cancellationToken: CancellationToken.None))
		{
		}

		resourceManager.UpdateResource(
			resourceId: resource,
			bucketResponse: new DiscordBucketResponse(
				bucket: bucket,
				limit: 2,
				remaining: 1,
				discordReset: discordReset,
				resetAfter: resetAfter),
			rateLimitResponse: null);

		using var reservation1 = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 1,
			cancellationToken: CancellationToken.None);
		DateTime.UtcNow.Should()
			.BeBefore(expected: reset);

		resourceManager.UpdateResource(
			resourceId: resource,
			bucketResponse: new DiscordBucketResponse(
				bucket: bucket,
				limit: 2,
				remaining: 0,
				discordReset: discordReset,
				resetAfter: resetAfter),
			rateLimitResponse: null);

		using var reservation2 = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 2,
			cancellationToken: CancellationToken.None);
		DateTime.UtcNow.Should()
			.BeAfter(expected: reset);
	}

	[Fact]
	public async Task GetReservationAsync_ShouldWait_WhenDiscoveryIsInProgress()
	{
		var resourceManager = new DiscordResourceManager(logger: null);
		resourceManager.Start();

		var resource = new DiscordResourceId(HttpMethod: "GET", Path: "channels/123");
		var bucket = Guid.NewGuid()
			.ToString();

		using (var _ = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 0,
			cancellationToken: CancellationToken.None))
		{
		}

		// Make bucket obsolete
		resourceManager.UpdateResource(
			resourceId: resource,
			bucketResponse: new DiscordBucketResponse(
				bucket: bucket,
				limit: 1,
				remaining: 0,
				discordReset: 0,
				resetAfter: TimeSpan.Zero),
			rateLimitResponse: null);
		await Task.Delay(delay: TimeSpan.FromMilliseconds(value: 10));

		var discoveryReservation = await resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 1,
			cancellationToken: CancellationToken.None);
		var hangingReservation = resourceManager.GetReservationAsync(
			resourceId: resource,
			requestIndex: 2,
			cancellationToken: CancellationToken.None);

		await Task.Delay(delay: TimeSpan.FromMilliseconds(value: 10));
		hangingReservation.Status.Should()
			.Be(expected: TaskStatus.WaitingForActivation);

		discoveryReservation.Dispose();

		await Task.Delay(delay: TimeSpan.FromMilliseconds(value: 10));
		hangingReservation.Status.Should()
			.Be(expected: TaskStatus.RanToCompletion);
	}

	[Theory]
	[InlineData("GET", "channels/123", "GET:channels/123")]
	[InlineData("GET", "channels/123/messages", "GET:channels/123/messages")]
	[InlineData("GET", "channels/123/messages/123", "GET:channels/123/messages/")]
	[InlineData("GET", "channels/123/messages/abc", "GET:channels/123/messages/abc")]
	[InlineData("GET", "channels/123/messages/123/abc", "GET:channels/123/messages//abc")]
	[InlineData("GET", "guilds/123", "GET:guilds/123")]
	[InlineData("GET", "guilds/123/channels", "GET:guilds/123/channels")]
	[InlineData("GET", "guilds/123/channels/123", "GET:guilds/123/channels/")]
	[InlineData("GET", "guilds/123/channels/abc", "GET:guilds/123/channels/abc")]
	[InlineData("GET", "guilds/123/channels/123/abc", "GET:guilds/123/channels//abc")]
	[InlineData("GET", "webhooks/123", "GET:webhooks/123")]
	[InlineData("GET", "webhooks/123/token_abc.123", "GET:webhooks/123/token_abc.123")]
	[InlineData("GET", "webhooks/123/token_abc.123/123", "GET:webhooks/123/token_abc.123/")]
	[InlineData("GET", "webhooks/123/token_abc.123/abc", "GET:webhooks/123/token_abc.123/abc")]
	[InlineData("GET", "webhooks/123/123", "GET:webhooks/123/123")]
	[InlineData("GET", "webhooks/123/123/123", "GET:webhooks/123/123/")]
	[InlineData("GET", "webhooks/123/123/abc", "GET:webhooks/123/123/abc")]
	[InlineData("GET", "interactions/123/abc/callback", "GET:interactions///callback")]
	[InlineData("GET", "users/123", "GET:users/")]
	[InlineData("GET", "users/123/123", "GET:users//")]
	[InlineData("GET", "users/123/abc", "GET:users//abc")]
	[InlineData("GET", "users/abc/123", "GET:users/abc/")]
	[InlineData("GET", "channels/123?query", "GET:channels/123")]
	[InlineData("GET", "channels/123/123?query", "GET:channels/123/")]
	[InlineData("GET", "channels/123/reactions/url_encoded/123", "GET:channels/123/reactions/url_encoded/")]
	[InlineData("GET", "channels/123/reactions/url%encoded/123", "GET:channels/123/reactions//")]
	[InlineData("GET", "channels/123/reactions/url%encoded/@me", "GET:channels/123/reactions//@me")]
	public void GetResource(string httpMethod, string endpoint, string expectedResource)
	{
		var resourceManager = new DiscordResourceManager(logger: null);

		var resource = resourceManager.GetResourceId(httpMethod: httpMethod, endpoint: endpoint);

		resource.ToString()
			.Should()
			.Be(expected: expectedResource);
	}
}