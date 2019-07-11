using System;
using System.IO;
using System.Threading.Tasks;
using Etdb.ServiceBase.Services.Abstractions;
using Xunit;

namespace Etdb.ServiceBase.Services.Tests
{
    public class ImageCompressionIntegrationTests
    {
        private readonly ImageCompressionService imageCompressionService;
        private const string FileName = "kitten.jpg";
        private readonly string basePath = Path.Combine(AppContext.BaseDirectory, "Files");
        private readonly IFileService fileService;

        public ImageCompressionIntegrationTests()
        {
            this.imageCompressionService = new ImageCompressionService();
            this.fileService = new FileService();
        }

        [Theory]
        [InlineData("kitten.jpg")]
        [InlineData("largeimage.jpg")]
        public async Task ImageCompressionService_Compress_Lowers_Image_Size_Succeeds(string fileName)
        {
            var originalBytes = File.ReadAllBytes(Path.Combine(this.basePath, fileName));
            var compressedBytes = this.imageCompressionService.Compress(originalBytes, "image/jpeg");

            Assert.NotEqual(originalBytes, compressedBytes);

            await this.fileService.StoreBinaryAsync(this.basePath, $"compressed_{fileName}_{DateTime.UtcNow.Ticks}.jpg",
                compressedBytes);
        }

        [Fact]
        public async Task ImageCompressionService_Compress_Vector_Returns_Same_Size_Array()
        {
            var originalBytes = File.ReadAllBytes(Path.Combine(this.basePath, "google.svg"));
            var compressedBytes = this.imageCompressionService.Compress(originalBytes, "image/svg+xml");

            Assert.Equal(originalBytes, compressedBytes);

            await this.fileService.StoreBinaryAsync(this.basePath,
                $"uncompressed_googlesvg_{DateTime.UtcNow.Ticks}.svg",
                compressedBytes);
        }
        
        [Theory]
        [InlineData("kitten.jpg")]
        [InlineData("largeimage.jpg")]
        public async Task ImageCompressionService_Resize_Lowers_Image_Size(string fileName)
        {
            var originalBytes = File.ReadAllBytes(Path.Combine(this.basePath, fileName));
            var resizedBytes = this.imageCompressionService.Resize(originalBytes, "image/jpeg", 256, 256);

            Assert.NotEqual(originalBytes, resizedBytes);

            await this.fileService.StoreBinaryAsync(this.basePath,
                $"resized_{fileName}_{DateTime.UtcNow.Ticks}.jpg",
                resizedBytes);
        }

        [Fact]
        public async Task ImageCompressionService_Resize_Vector_Returns_Same_Size_Array()
        {
            var originalBytes = File.ReadAllBytes(Path.Combine(this.basePath, "google.svg"));
            var compressedBytes = this.imageCompressionService.Resize(originalBytes, "image/svg+xml", 256, 256);

            Assert.Equal(originalBytes, compressedBytes);

            await this.fileService.StoreBinaryAsync(this.basePath, $"not_resized_googlesvg_{DateTime.UtcNow.Ticks}.svg",
                compressedBytes);
        }
    }
}