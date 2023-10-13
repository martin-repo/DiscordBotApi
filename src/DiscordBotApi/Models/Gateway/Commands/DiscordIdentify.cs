﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentify.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordIdentify(
	string Token,
	DiscordGatewayConnectionProperties Properties,
	DiscordShard? Shard,
	int Intents
);