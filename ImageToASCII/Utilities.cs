using System.Drawing;

namespace ImageToASCII;

public static class Utilities
{
    public static void ResizeBitmap(ref Bitmap bitmap, int maxHeight, float offsetWidth)
    {
        float aspectRatio = (float)bitmap.Width / bitmap.Height;
        int newWidth = (int)(maxHeight * aspectRatio * offsetWidth);

        if (bitmap.Height > maxHeight || bitmap.Width > newWidth)
            bitmap = new Bitmap(bitmap, new Size(newWidth, maxHeight));
    }

    public static Bitmap FileToBitmap(string filePath)
    {
        using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return new Bitmap(fileStream);
    }
}