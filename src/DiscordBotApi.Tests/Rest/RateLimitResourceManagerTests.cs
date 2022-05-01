// -------------------------------------------------------------------------------------------------
// <copyright file="RateLimitResourceManagerTests.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Tests.Rest
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using DiscordBotApi.Models.Rest;
    using DiscordBotApi.Rest;
    using DiscordBotApi.Utilities;

    using FluentAssertions;

    using Xunit;

    public class RateLimitResourceManagerTests
    {
        [Fact]
        public async Task GetReservationAsync_ShouldWait_WhenBucketExistsAndIsEmpty()
        {
            var resourceManager = new DiscordResourceManager(null);
            resourceManager.Start();

            var resource = new DiscordResourceId("GET", "channels/123");
            var resetAfter = TimeSpan.FromMilliseconds(100);
            var reset = DateTime.UtcNow.Add(resetAfter);
            var bucket = Guid.NewGuid().ToString();

            using (var _ = await resourceManager.GetReservationAsync(resource, 0, CancellationToken.None))
            {
            }

            resourceManager.UpdateResource(resource, new DiscordBucketResponse(bucket, 1, 0, 0, resetAfter), null);

            using var reservation = await resourceManager.GetReservationAsync(resource, 1, CancellationToken.None);

            DateTime.UtcNow.Should().BeAfter(reset);
        }

        [Fact]
        public async Task GetReservationAsync_ShouldWait_WhenBucketIsUpdated()
        {
            var resourceManager = new DiscordResourceManager(null);
            resourceManager.Start();

            var resource = new DiscordResourceId("GET", "channels/123");
            var resetAfter = TimeSpan.FromMilliseconds(100);
            var reset = DateTime.UtcNow.Add(resetAfter);
            var discordReset = DateTimeUtils.ToEpochTimeSeconds(reset);
            var bucket = Guid.NewGuid().ToString();

            using (var _ = await resourceManager.GetReservationAsync(resource, 0, CancellationToken.None))
            {
            }

            resourceManager.UpdateResource(resource, new DiscordBucketResponse(bucket, 2, 1, discordReset, resetAfter), null);

            using var reservation1 = await resourceManager.GetReservationAsync(resource, 1, CancellationToken.None);
            DateTime.UtcNow.Should().BeBefore(reset);

            resourceManager.UpdateResource(resource, new DiscordBucketResponse(bucket, 2, 0, discordReset, resetAfter), null);

            using var reservation2 = await resourceManager.GetReservationAsync(resource, 2, CancellationToken.None);
            DateTime.UtcNow.Should().BeAfter(reset);
        }

        [Fact]
        public async Task GetReservationAsync_ShouldWait_WhenDiscoveryIsInProgress()
        {
            var resourceManager = new DiscordResourceManager(null);
            resourceManager.Start();

            var resource = new DiscordResourceId("GET", "channels/123");
            var bucket = Guid.NewGuid().ToString();

            using (var _ = await resourceManager.GetReservationAsync(resource, 0, CancellationToken.None))
            {
            }

            // Make bucket obsolete
            resourceManager.UpdateResource(resource, new DiscordBucketResponse(bucket, 1, 0, 0, TimeSpan.Zero), null);
            await Task.Delay(TimeSpan.FromMilliseconds(10));

            var discoveryReservation = await resourceManager.GetReservationAsync(resource, 1, CancellationToken.None);
            var hangingReservation = resourceManager.GetReservationAsync(resource, 2, CancellationToken.None);

            await Task.Delay(TimeSpan.FromMilliseconds(10));
            hangingReservation.Status.Should().Be(TaskStatus.WaitingForActivation);

            discoveryReservation.Dispose();

            await Task.Delay(TimeSpan.FromMilliseconds(10));
            hangingReservation.Status.Should().Be(TaskStatus.RanToCompletion);
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
            var resourceManager = new DiscordResourceManager(null);

            var resource = resourceManager.GetResourceId(httpMethod, endpoint);

            resource.ToString().Should().Be(expectedResource);
        }
    }
}