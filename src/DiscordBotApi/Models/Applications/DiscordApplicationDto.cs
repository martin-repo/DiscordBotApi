// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/resources/application#application-object-application-structure
internal sealed record DiscordApplicationDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "bot_public")]
	bool BotPublic,
	[property: JsonPropertyName(name: "bot_require_code_grant")]
	bool BotRequireCodeGrant,
	[property: JsonPropertyName(name: "owner")]
	DiscordUserDto? Owner,
	[property: JsonPropertyName(name: "flags")]
	uint? Flags,
	[property: JsonPropertyName(name: "tags")]
	string[]? Tags,
	[property: JsonPropertyName(name: "install_params")]
	DiscordInstallParamsDto? InstallParams,
	[property: JsonPropertyName(name: "custom_install_url")]
	string? CustomInstallUrl
)
{
	public DiscordApplication ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Name = Name,
			BotPublic = BotPublic,
			BotRequireCodeGrant = BotRequireCodeGrant,
			Owner = Owner?.ToModel(),
			Flags = Flags != null ? (DiscordApplicationFlags)Flags : null,
			Tags = Tags,
			InstallParams = InstallParams?.ToModel(),
			CustomInstallUrl = CustomInstallUrl
		};
}