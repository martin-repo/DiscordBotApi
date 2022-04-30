// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUser.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Users
{
    public record DiscordUser
    {
        internal DiscordUser(DiscordUserDto dto)
        {
            Id = ulong.Parse(dto.Id);
            Username = dto.Username;
            Discriminator = dto.Discriminator;
            Avatar = dto.Avatar;
            Bot = dto.Bot;
        }

        public string? Avatar { get; init; }

        public bool? Bot { get; init; }

        public string Discriminator { get; init; }

        public ulong Id { get; init; }

        public string Username { get; init; }
    }
}