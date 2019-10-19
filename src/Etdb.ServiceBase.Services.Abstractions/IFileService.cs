using System;
using System.Threading.Tasks;

namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IFileService
    {
        Task<byte[]> ReadBinaryAsync(string basePath, string fileName);

        Task<byte[]> ReadBinaryAsync(string fullPath);

        Task StoreBinaryAsync(string basePath, string fileName, byte[] fileBytes);

        ValueTask StoreBinaryAsync(string basePath, string fileName, ReadOnlyMemory<byte> fileBytes);

        Task StoreBinaryAsync(string fullPath, byte[] fileBytes);

        ValueTask StoreBinaryAsync(string fullPath, ReadOnlyMemory<byte> fileBytes);

        void DeleteBinary(string basePath, string fileName);

        void DeleteBinary(string fullPath);
    }
}