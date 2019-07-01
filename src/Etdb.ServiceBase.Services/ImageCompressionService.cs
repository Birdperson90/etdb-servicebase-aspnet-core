using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Etdb.ServiceBase.Services.Abstractions;

namespace Etdb.ServiceBase.Services
{
    public class ImageCompressionService : IImageCompressionService
    {
        public byte[] Compress(byte[] bytes, string mimeType, byte encodeValue = 75)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                var image = Image.FromStream(memoryStream);

                var encoder = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? Encoder.Quality
                    : Encoder.Compression;

                var encoderParameter = new EncoderParameter(encoder, encodeValue);

                return Compress(image, GetImageCodecInfo(mimeType), encoderParameter);
            }
        }

        private static byte[] Compress(Image image, ImageCodecInfo codecInfo, EncoderParameter encoderParameter)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, codecInfo, new EncoderParameters
                {
                    Param =
                    {
                        [0] = encoderParameter
                    }
                });

                return stream.ToArray();
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
            => ImageCodecInfo.GetImageEncoders().First(codec =>
                codec.MimeType.Equals(mimeType, StringComparison.InvariantCultureIgnoreCase));
    }
}