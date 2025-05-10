// -------------------------------------------------------------------------------------------------
// <copyright file="RoleTests.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Interface.Models.Guilds;

using FluentAssertions;

using Xunit;

namespace DiscordBotApi.EndToEndTests.Guild;

[Collection(name: "DiscordBotClient collection")]
public class RoleTests
{
	private readonly DiscordBotClient _client;
	private readonly ulong _guildId;

	public RoleTests(DiscordBotClientFixture fixture)
	{
		_client = fixture.BotClient;
		_guildId = fixture.GuildId;
	}

	[Theory]
	[AutoData]
	private async Task CreateGuildRoleAsync(string name, DiscordPermissions permissions)
	{
		var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleCreate>();

		void OnGuildRoleCreate(object? sender, DiscordGuildRoleCreate eventArgs) => completion.SetResult(result: eventArgs);

		void ValidateRole(DiscordRole role)
		{
			role.Name.Should()
				.Be(expected: name);
			role.Permissions.Should()
				.Be(expected: permissions);
		}

		_client.GuildRoleCreate += OnGuildRoleCreate;
		try
		{
			var roleFromRest = await _client.CreateGuildRoleAsync(
				guildId: _guildId,
				args: new DiscordCreateGuildRoleArgs
				{
					Name = name,
					Permissions = permissions
				});
			var roleFromGateway = await completion.Task;

			typeof(DiscordRole).GetProperties()
				.Length.Should()
				.Be(expected: 3);
			ValidateRole(role: roleFromRest);
			ValidateRole(role: roleFromGateway.Role);
		}
		finally
		{
			_client.GuildRoleCreate -= OnGuildRoleCreate;
		}
	}

	[Fact]
	private async Task DeleteGuildRoleAsync()
	{
		var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleDelete>();

		void OnGuildRoleDelete(object? sender, DiscordGuildRoleDelete eventArgs) => completion.SetResult(result: eventArgs);

		_client.GuildRoleDelete += OnGuildRoleDelete;
		try
		{
			var role = await _client.CreateGuildRoleAsync(
				guildId: _guildId,
				args: new DiscordCreateGuildRoleArgs
				{
					Name = Guid.NewGuid()
						.ToString(format: "D"),
					Permissions = DiscordPermissions.None
				});

			await role.DeleteAsync();
			var guildRole = await completion.Task;

			guildRole.GuildId.Should()
				.Be(expected: _guildId);
			guildRole.RoleId.Should()
				.Be(expected: role.Id);
		}
		finally
		{
			_client.GuildRoleDelete -= OnGuildRoleDelete;
		}
	}

	[Theory]
	[AutoData]
	private async Task ModifyGuildRoleAsync(string name, DiscordPermissions permissions)
	{
		var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleUpdate>();

		void OnGuildRoleUpdate(object? sender, DiscordGuildRoleUpdate eventArgs) => completion.SetResult(result: eventArgs);

		void ValidateRole(DiscordRole role)
		{
			role.Name.Should()
				.Be(expected: name);
			role.Permissions.Should()
				.Be(expected: permissions);
		}

		_client.GuildRoleUpdate += OnGuildRoleUpdate;
		try
		{
			var roleFromRest = await _client.CreateGuildRoleAsync(
				guildId: _guildId,
				args: new DiscordCreateGuildRoleArgs
				{
					Name = Guid.NewGuid()
						.ToString(format: "D"),
					Permissions = DiscordPermissions.None
				});

			roleFromRest = await roleFromRest.ModifyAsync(
				args: new DiscordModifyGuildRoleArgs
				{
					Name = name,
					Permissions = permissions
				});
			var roleFromGateway = await completion.Task;

			typeof(DiscordRole).GetProperties()
				.Length.Should()
				.Be(expected: 3);
			ValidateRole(role: roleFromRest);
			ValidateRole(role: roleFromGateway.Role);
		}
		finally
		{
			_client.GuildRoleUpdate -= OnGuildRoleUpdate;
		}
	}
}