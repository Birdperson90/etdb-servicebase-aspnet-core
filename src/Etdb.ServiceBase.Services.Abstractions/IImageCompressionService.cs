namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IImageCompressionService
    {
        byte[] Compress(byte[] bytes, string mimeType, long encodeValue = 75);

        byte[] CreateThumbnail(byte[] bytes, string mimeType);
    }
}