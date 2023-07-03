using System.Drawing;
using ImageToASCII;

string imagePath = "C:\\Users\\User\\Desktop\\50236551.png";
// string imagePath = "C:\\Users\\User\\Desktop\\50236551 - Copy (2).png";

using FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
Bitmap bitmap = new Bitmap(fileStream);

float aspectRatio = (float)bitmap.Width / bitmap.Height;
int maxHeight = 170;
int newWidth = (int)(maxHeight * aspectRatio * 2.5f);

if (bitmap.Height > maxHeight || bitmap.Width > newWidth)
    bitmap = new Bitmap(bitmap, new Size() { Width = newWidth, Height = maxHeight });

bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
bitmap.ToGrayScale();

for (int x = 0, width = bitmap.Width; x < width; x++)
{
    for (int y = 0, height = bitmap.Height; y < height; y++)
    {
        Color color = bitmap.GetPixel(x, y);

        Console.Write(color.R > 162 ? 'O' : 'H');
    }

    Console.WriteLine();
}

Console.ReadLine();