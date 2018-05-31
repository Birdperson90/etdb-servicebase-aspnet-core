using System;
using System.IO;
using System.Threading.Tasks;
using Etdb.ServiceBase.Services.Abstractions;

namespace Etdb.ServiceBase.Services
{
    public class FileService : IFileService
    {
        public async Task<byte[]> ReadBinaryAsync(string basePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            
            var fullPath = Path.Combine(basePath, fileName);
            
            return await ReadBytes(fullPath);
        }
        
        public async Task<byte[]> ReadBinaryAsync(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }
            
            return await ReadBytes(fullPath);
        }

        public async Task StoreBinaryAsync(string basePath, string fileName, byte[] fileBytes)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            
            var fullPath = Path.Combine(basePath, fileName);

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            await StoreBytes(fullPath, fileBytes);
        }

        public async Task StoreBinaryAsync(string fullPath, byte[] fileBytes)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            await StoreBytes(fullPath, fileBytes);
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
            
            DeleteFile(Path.Combine(basePath, fileName));
        }

        public void DeleteBinary(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            DeleteFile(fullPath);
        }

        private static async Task<byte[]> ReadBytes(string path)
        {
            byte[] fileBytes;
            
            using (var binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                var offest = 0;
                var totalLength = binaryReader.BaseStream.Length;
                fileBytes = new byte[totalLength];

                while (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position > 0)
                {
                    var leftOver = totalLength - offest;
                    offest += await binaryReader.BaseStream.ReadAsync(fileBytes, offest, leftOver < 4096L ? (int) leftOver : 4096);
                }
            }

            return fileBytes;
        }
        
        private static async Task StoreBytes(string path, byte[] fileBytes)
        {
            using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Write)))
            {
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
        }

        private static void DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            
            File.Delete(path);
        }
    }
}