using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Etdb.ServiceBase.Services.Abstractions;

namespace Etdb.ServiceBase.Services
{
    public class ImageCompressionService : IImageCompressionService
    {
        public byte[] Compress(byte[] bytes, string mimeType, long compressionValue = 75)
        {
            using var memoryStream = new MemoryStream(bytes);
            Image image;
            try
            {
                image = Image.FromStream(memoryStream);
            }
            catch (Exception)
            {
                return bytes;
            }

            var encoder = GetMatchingPlatformEncoder();

            var encoderParameter = new EncoderParameter(encoder, compressionValue);

            var codecInfo = GetImageCodecInfo(mimeType);

            return Compress(image, codecInfo, encoderParameter);
        }

        public byte[] Resize(byte[] bytes, string mimeType, int width = 256, int height = 256,
            long compressionValue = 75)
        {
            Image image;

            using (var memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    image = Image.FromStream(memoryStream);
                }
                catch (Exception)
                {
                    return bytes;
                }
            }

            RotateImage(image);

            var (dimensionX, dimensionY) = CalculateDimensions(width, height, image);

            var resizedImage = new Bitmap(dimensionX, dimensionY);

            const float dpi = 72;

            resizedImage.SetResolution(dpi, dpi);

            DrawGraphics(resizedImage, image, dimensionX, dimensionY);

            return Compress(resizedImage, GetImageCodecInfo(mimeType),
                new EncoderParameter(GetMatchingPlatformEncoder(), compressionValue));
        }

        private static void DrawGraphics(Image resizedImage, Image image, int dimensionX, int dimensionY)
        {
            using var graphics = Graphics.FromImage(resizedImage);

            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphics.DrawImage(image, 0, 0, dimensionX, dimensionY);
        }

        private static void RotateImage(Image image)
        {
            var orientationImageProperty = image.PropertyItems.FirstOrDefault(prop => prop.Id == 0x0112);

            if (orientationImageProperty == null) return;

            int orientationValue = image.GetPropertyItem(orientationImageProperty.Id).Value[0];

            var rotateFlipType = GetRotateFlipType(orientationValue);

            image.RotateFlip(rotateFlipType);
        }

        private static byte[] Compress(Image image, ImageCodecInfo codecInfo, EncoderParameter encoderParameter)
        {
            using var stream = new MemoryStream();

            image.Save(stream, codecInfo, new EncoderParameters
            {
                Param =
                {
                    [0] = encoderParameter
                }
            });

            return stream.ToArray();
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

        private static (int newWidth, int newHeight) CalculateDimensions(int width, int height, Image image)
        {
            if (image.Width <= width && image.Height <= height) return (width, height);

            var ratioX = (double) width / image.Width;
            var ratioY = (double) height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            return ((int) (image.Width * ratio), (int) (image.Height * ratio));
        }

        private static RotateFlipType GetRotateFlipType(int rotateValue)
            => rotateValue switch
            {
                1 => RotateFlipType.RotateNoneFlipNone,
                2 => RotateFlipType.RotateNoneFlipX,
                3 => RotateFlipType.Rotate180FlipNone,
                4 => RotateFlipType.Rotate180FlipX,
                5 => RotateFlipType.Rotate90FlipX,
                6 => RotateFlipType.Rotate90FlipNone,
                7 => RotateFlipType.Rotate270FlipX,
                8 => RotateFlipType.Rotate270FlipNone,
                _ => RotateFlipType.RotateNoneFlipNone
            };

        private static Encoder GetMatchingPlatformEncoder()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Encoder.Quality
                : Encoder.Compression;
    }
}