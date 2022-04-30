// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/user#create-dm-json-params
    internal record DiscordCreateDmArgsDto([property: JsonPropertyName("recipient_id")] string ReceipientId)
    {
        internal DiscordCreateDmArgsDto(DiscordCreateDmArgs model)
            : this(model.RecipientId.ToString())
        {
        }
    }
}