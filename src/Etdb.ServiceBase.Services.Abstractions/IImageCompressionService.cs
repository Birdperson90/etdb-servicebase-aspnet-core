namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IImageCompressionService
    {
        byte[] Compress(byte[] bytes, string mimeType);
    }
}