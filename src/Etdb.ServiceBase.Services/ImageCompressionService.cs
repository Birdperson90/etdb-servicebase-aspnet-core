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
        public byte[] Compress(byte[] bytes, string mimeType, long encodeValue = 75)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                Image image;
                try
                {
                    image = Image.FromStream(memoryStream);
                }
                catch (Exception)
                {
                    // this means it's an SVG or some other vector-graphic!
                    return bytes;
                }

                var encoder = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? Encoder.Quality
                    : Encoder.Compression;

                var encoderParameter = new EncoderParameter(encoder, encodeValue);

                var codecInfo = GetImageCodecInfo(mimeType);

                return Compress(image, codecInfo, encoderParameter);
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
        {
            var codecInfos = ImageCodecInfo.GetImageEncoders();

            var codedcInfo = codecInfos.FirstOrDefault(codec =>
                codec.MimeType.Equals(mimeType, StringComparison.InvariantCultureIgnoreCase));

            if (codedcInfo != null) return codedcInfo;

            return codecInfos.FirstOrDefault(codec =>
                codec.MimeType.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}