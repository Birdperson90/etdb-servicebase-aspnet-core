namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IImageCompressionService
    {
        byte[] Compress(byte[] bytes, string mimeType, long compressionValue = 75);

        byte[] Resize(byte[] bytes, string mimeType, int width = 256, int height = 256, long compressionValue = 75);
    }
}