// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordJsonErrorCode.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    // https://discord.com/developers/docs/topics/opcodes-and-status-codes#json
    public enum DiscordJsonErrorCode
    {
        GeneralError = 0,
        UnknownMessage = 10008,
        CannotSendMessagesToThisUser = 50007
    }
}