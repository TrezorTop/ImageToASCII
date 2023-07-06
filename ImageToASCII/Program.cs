using System.Drawing;
using ImageToASCII;

const int maxHeight = 170;
const float offsetWidth = 2.5f;
char[] chars = { ' ', '.', ':', '+', '*', 'o', 'x', '8', 'B', 'M', 'W', 'X', '&', '$', '%', '#', '@' };

string imagePath = "C:\\Users\\User\\Desktop\\photo.jpg";
// string imagePath = "C:\\Users\\User\\Desktop\\50236551 - Copy (2).png";

using FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
Bitmap bitmap = new Bitmap(fileStream);

ResizeBitmap(ref bitmap);
bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
bitmap.ToGrayScale();

int step = 255 / chars.Length;

for (int x = 0, width = bitmap.Width; x < width; x++)
{
    for (int y = 0, height = bitmap.Height; y < height; y++)
    {
        Color color = bitmap.GetPixel(x, y);

        Console.Write(chars[color.R / step]);
    }

    Console.WriteLine();
}

Console.ReadLine();

void ResizeBitmap(ref Bitmap bitmap)
{
    float aspectRatio = (float)bitmap.Width / bitmap.Height;
    int newWidth = (int)(maxHeight * aspectRatio * offsetWidth);

    if (bitmap.Height > maxHeight || bitmap.Width > newWidth)
        bitmap = new Bitmap(bitmap, new Size() { Width = newWidth, Height = maxHeight });
}