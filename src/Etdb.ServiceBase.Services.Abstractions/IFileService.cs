using System.Threading.Tasks;

namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IFileService
    {
        Task<byte[]> ReadBinaryAsync(string basePath, string fileName);

        Task<byte[]> ReadBinaryAsync(string fullPath);

        Task StoreBinaryAsync(string basePath, string fileName, byte[] bytes);
    }
}