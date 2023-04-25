// -------------------------------------------------------------------------------------------------
// <copyright file="IZlibContext.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway;

internal interface IZlibContext
{
	Task<byte[]> DecompressAsync(byte[] compressedBytes, CancellationToken cancellationToken);
}