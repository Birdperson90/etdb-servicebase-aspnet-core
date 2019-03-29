using System;
using System.IO;
using System.Threading.Tasks;
using Etdb.ServiceBase.Services.Abstractions;
using Xunit;

namespace Etdb.ServiceBase.Services.Tests
{
    public class FileServiceTests
    {
        private readonly IFileService fileService;
        private const string FileName = "kitten.jpg";
        private readonly string basePath = Path.Combine(AppContext.BaseDirectory, "Files");

        public FileServiceTests()
        {
            this.fileService = new FileService();
        }

        [Fact]
        public async Task FileService_ReadBinary_ExpectBinaryContent()
        {
            var binary = await this.fileService.ReadBinaryAsync(this.basePath, FileServiceTests.FileName);

            var storedFileName = $"{DateTime.UtcNow.Ticks}_{FileServiceTests.FileName}";

            await this.fileService.StoreBinaryAsync(this.basePath, storedFileName, binary);

            var secondBinary = await this.fileService.ReadBinaryAsync(this.basePath, storedFileName);

            Assert.Equal(binary, secondBinary);
        }
    }
}