using SkiaSharp;
using System.IO;

namespace SKSurfaceMinRepro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SKImageInfo imageInfo = new(512, 384);
            SKSurface surface = SKSurface.Create(imageInfo);
            SKCanvas firstCanvas = surface.Canvas;
            SKBitmap sourceBitmap = SKBitmap.Decode("lizmelo-painting.png");

            firstCanvas.DrawBitmap(sourceBitmap,
                new SKRect(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                new SKRect(0, 0, 512, 384)
                );
            firstCanvas.Flush();

            SKBitmap finalBitmap = new(256, 192);
            SKCanvas secondCanvas = new(finalBitmap);
            SKRect surfaceRect = new(0, 0, 256, 192);
            secondCanvas.DrawImage(surface.Snapshot(), surfaceRect, new SKRect(0, 0, 256, 192));
            secondCanvas.Flush();

            using FileStream fs = File.Create("output.png");
            finalBitmap.Encode(fs, SKEncodedImageFormat.Png, 1);
        }
    }
}
