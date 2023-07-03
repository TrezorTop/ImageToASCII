using System.Drawing;

namespace ImageToASCII;

public static class Extensions
{
    public static void ToGrayScale(this Bitmap bitmap)
    {
        for (int x = 0, width = bitmap.Width; x < width; x++)
        {
            for (int y = 0, height = bitmap.Height; y < height; y++)
            {
                Color pixel = bitmap.GetPixel(x, y);
                int average = (pixel.R + pixel.G + pixel.B) / 3;
                bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, average, average, average));
            }
        }
    }
}