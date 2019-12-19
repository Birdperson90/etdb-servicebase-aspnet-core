using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Etdb.ServiceBase.Services.Abstractions;

namespace Etdb.ServiceBase.Services
{
    public class FileService : IFileService
    {
        public Task<byte[]> ReadBinaryAsync(string basePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(basePath)) throw new ArgumentNullException(nameof(basePath));

            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            var fullPath = Path.Combine(basePath, fileName);

            return ReadBinaryInternalAsync(fullPath);
        }

        public Task<byte[]> ReadBinaryAsync(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath)) throw new ArgumentNullException(nameof(fullPath));

            return ReadBinaryInternalAsync(fullPath);
        }

        public async Task StoreBinaryAsync(string basePath, string fileName, byte[] fileBytes)
        {
            if (string.IsNullOrWhiteSpace(basePath)) throw new ArgumentNullException(nameof(basePath));

            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

            var fullPath = Path.Combine(basePath, fileName);

            await StoreBinaryInternalAsync(fullPath, fileBytes);
        }

        public ValueTask StoreBinaryAsync(string basePath, string fileName, ReadOnlyMemory<byte> fileBytes)
        {
            if (string.IsNullOrWhiteSpace(basePath)) throw new ArgumentNullException(nameof(basePath));

            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

            var fullPath = Path.Combine(basePath, fileName);

            return StoreBinaryInternalAsync(fullPath, fileBytes);
        }

        public async Task StoreBinaryAsync(string fullPath, byte[] fileBytes)
        {
            if (string.IsNullOrWhiteSpace(fullPath)) throw new ArgumentNullException(nameof(fullPath));

            CreateDirectory(fullPath);

            await StoreBinaryInternalAsync(fullPath, fileBytes);
        }

        public ValueTask StoreBinaryAsync(string fullPath, ReadOnlyMemory<byte> fileBytes)
        {
            if (string.IsNullOrWhiteSpace(fullPath)) throw new ArgumentNullException(nameof(fullPath));

            CreateDirectory(fullPath);

            return StoreBinaryInternalAsync(fullPath, fileBytes);
        }

        public void DeleteBinary(string basePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            DeleteBinaryInternal(Path.Combine(basePath, fileName));
        }

        public void DeleteBinary(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            DeleteBinaryInternal(fullPath);
        }

        private static async Task<byte[]> ReadBinaryInternalAsync(string path)
        {
            byte[] fileBytes;

            using (var binaryReader =
                new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                var offest = 0;
                var totalLength = binaryReader.BaseStream.Length;
                fileBytes = new byte[totalLength];

                while (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position > 0)
                {
                    var leftOver = totalLength - offest;
                    offest += await binaryReader.BaseStream.ReadAsync(fileBytes, offest,
                        leftOver < 4096L ? (int) leftOver : 4096);
                }
            }

            return fileBytes;
        }

        private static async Task StoreBinaryInternalAsync(string path, byte[] fileBytes)
        {
            await using var binaryWriter =
                new BinaryWriter(new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Write));

            var offset = 0;
            var totalLength = fileBytes.LongLength;

            while (totalLength - binaryWriter.BaseStream.Position > 0)
            {
                var leftOver = totalLength - offset;

                var chunkSize = leftOver <= 4096L ? (int) leftOver : 4096;

                await binaryWriter.BaseStream.WriteAsync(fileBytes, offset, chunkSize);

                offset += chunkSize;
            }
        }

        private static async ValueTask StoreBinaryInternalAsync(string path, ReadOnlyMemory<byte> fileBytes)
        {
            await using var binaryWriter =
                new BinaryWriter(new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Write));

            await binaryWriter.BaseStream.WriteAsync(fileBytes);
        }

        private static void DeleteBinaryInternal(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            File.Delete(path);
        }

        private static void CreateDirectory(string fullPath)
        {
            var pathSeperator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "\\"
                : "/";

            var lastIndex = fullPath.LastIndexOf(pathSeperator, StringComparison.InvariantCultureIgnoreCase);

            var wantedDirectory = fullPath.Substring(0, lastIndex - 1);

            if (Directory.Exists(wantedDirectory)) return;

            Directory.CreateDirectory(wantedDirectory);
        }
    }
}