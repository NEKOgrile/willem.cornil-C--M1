using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;

namespace optimisation_images
{
    public static class ImageHelper
    {
        public static void SaveResized(Image<Rgba32> src, int maxSize, string outputPath)
        {
            // Crée le dossier si nécessaire
            var directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Clone l'image et applique le redimensionnement
            using var clone = src.Clone(ctx =>
            {
                ctx.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(maxSize, maxSize),
                });
            });

            // Encoder JPEG avec qualité 85
            var encoder = new JpegEncoder { Quality = 85 };
            clone.Save(outputPath, encoder);
        }

    }
}
