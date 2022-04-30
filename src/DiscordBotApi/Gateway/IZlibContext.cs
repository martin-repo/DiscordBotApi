// -------------------------------------------------------------------------------------------------
// <copyright file="IZlibContext.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    internal interface IZlibContext
    {
        Task<byte[]> DecompressAsync(byte[] compressedBytes, CancellationToken cancellationToken);
    }
}