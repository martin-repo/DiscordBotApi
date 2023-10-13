// -------------------------------------------------------------------------------------------------
// <copyright file="ZlibContext.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.IO.Compression;

namespace DiscordBotApi.Gateway;

internal class ZlibContext : IZlibContext, IDisposable
{
	private const byte ZlibHeaderByte = 0x78;

	private static readonly byte[] ZlibSuffixBytes =
	{
		0x00,
		0x00,
		0xFF,
		0xFF
	};

	private readonly DeflateStream _decompressor;
	private readonly MemoryStream _decompressorStream;

	private bool _isHeaderRead;

	public ZlibContext()
	{
		_decompressorStream = new MemoryStream();
		_decompressor = new DeflateStream(stream: _decompressorStream, mode: CompressionMode.Decompress);
	}

	public async Task<byte[]> DecompressAsync(byte[] compressedBytes, CancellationToken cancellationToken)
	{
		if (compressedBytes.Length < 4)
		{
			throw new ArgumentException(message: "At least 4 bytes are required.", paramName: nameof(compressedBytes));
		}

		if (!compressedBytes[^4..]
			.SequenceEqual(second: ZlibSuffixBytes))
		{
			throw new ArgumentException(message: "zlib suffix not found.", paramName: nameof(compressedBytes));
		}

		if (!_isHeaderRead)
		{
			if (compressedBytes[0] != ZlibHeaderByte)
			{
				throw new ArgumentException(message: "zlib header not found.", paramName: nameof(compressedBytes));
			}

			// Skip header
			_decompressorStream.Write(buffer: compressedBytes[2..]);
			_isHeaderRead = true;
		}
		else
		{
			_decompressorStream.SetLength(value: 0);
			_decompressorStream.Write(buffer: compressedBytes);
		}

		_decompressorStream.Position = 0;

		await using var decompressedStream = new MemoryStream();
		await _decompressor.CopyToAsync(destination: decompressedStream, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		return decompressedStream.ToArray();
	}

	public void Dispose()
	{
		_decompressor.Dispose();
		_decompressorStream.Dispose();

		GC.SuppressFinalize(obj: this);
	}
}