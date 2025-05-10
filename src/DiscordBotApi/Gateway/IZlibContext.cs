// -------------------------------------------------------------------------------------------------
// <copyright file="IZlibContext.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway;

internal interface IZlibContext
{
	Task<byte[]> DecompressAsync(byte[] compressedBytes, CancellationToken cancellationToken);
}