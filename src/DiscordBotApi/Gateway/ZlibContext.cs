// -------------------------------------------------------------------------------------------------
// <copyright file="ZlibContext.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using System.IO.Compression;

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
            _decompressorStream = new();
            _decompressor = new(_decompressorStream, CompressionMode.Decompress);
        }

        public async Task<byte[]> DecompressAsync(byte[] compressedBytes, CancellationToken cancellationToken)
        {
            if (compressedBytes.Length < 4)
            {
                throw new ArgumentException("At least 4 bytes are required.", nameof(compressedBytes));
            }

            if (!compressedBytes[^4..].SequenceEqual(ZlibSuffixBytes))
            {
                throw new ArgumentException("zlib suffix not found.", nameof(compressedBytes));
            }

            if (!_isHeaderRead)
            {
                if (compressedBytes[0] != ZlibHeaderByte)
                {
                    throw new ArgumentException("zlib header not found.", nameof(compressedBytes));
                }

                // Skip header
                _decompressorStream.Write(compressedBytes[2..]);
                _isHeaderRead = true;
            }
            else
            {
                _decompressorStream.SetLength(0);
                _decompressorStream.Write(compressedBytes);
            }

            _decompressorStream.Position = 0;

            await using var decompressedStream = new MemoryStream();
            await _decompressor.CopyToAsync(decompressedStream, cancellationToken).ConfigureAwait(false);

            return decompressedStream.ToArray();
        }

        public void Dispose()
        {
            _decompressor.Dispose();
            _decompressorStream.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}