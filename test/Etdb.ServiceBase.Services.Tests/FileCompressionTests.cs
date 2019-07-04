using System;
using System.IO;
using System.Threading.Tasks;
using Etdb.ServiceBase.Services.Abstractions;
using Xunit;

namespace Etdb.ServiceBase.Services.Tests
{
    public class FileCompressionTests
    {
        private readonly IImageCompressionService imageCompressionService;
        private const string FileName = "kitten.jpg";
        private readonly string basePath = Path.Combine(AppContext.BaseDirectory, "Files");
        private readonly IFileService fileService;

        public FileCompressionTests()
        {
            this.imageCompressionService = new ImageCompressionService();
            this.fileService = new FileService();
        }

        [Theory]
        [InlineData("kitten.jpg")]
        [InlineData("largeimage.jpg")]
        public async Task FileCompressionService_Compress_Lowers_Image_Size_Succeeds(string fileName)
        {
            var originalBytes = File.ReadAllBytes(Path.Combine(this.basePath, fileName));
            var compressedBytes = this.imageCompressionService.Compress(originalBytes, "image/jpeg");

            Assert.NotEqual(originalBytes, compressedBytes);

            await this.fileService.StoreBinaryAsync(this.basePath, $"compressed_{fileName}_{DateTime.UtcNow.Ticks}.jpg",
                compressedBytes);
        }
    }
}