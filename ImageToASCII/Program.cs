using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

const int maxHeight = 170;
const float offsetWidth = 2.5f;
char[] chars = { ' ', '.', ':', '+', '*', 'o', 'x', '8', 'B', 'M', 'W', 'X', '&', '$', '%', '#', '@' };
int charsStep = Byte.MaxValue / chars.Length;

string imagePath = "C:\\Users\\User\\Desktop\\photo.jpg";

Bitmap bitmap = FileToBitmap(imagePath);

ResizeBitmap(ref bitmap);
bitmap.ToGrayScale();

StringBuilder stringBuilder = new StringBuilder("");

Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, bitmap.PixelFormat);

int stride = bitmapData.Stride; // number of bytes allocated for each scan line
int charsLenght = chars.Length;

try
{
    unsafe
    {
        byte* pointer = (byte*)bitmapData.Scan0;

        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                int offset = y * stride + x * 4; // pixel is represented by four bytes (RGBA)

                int charIndex = pointer[offset] / charsStep;

                if (charIndex >= charsLenght) charIndex = charsLenght - 1;
                if (charIndex < 0) charIndex = 0;

                stringBuilder.Append(chars[charIndex]);
            }

            stringBuilder.AppendLine();
        }
    }
}
finally
{
    bitmap.UnlockBits(bitmapData);
}

Console.WriteLine(stringBuilder);

Console.ReadLine();

void ResizeBitmap(ref Bitmap bitmap)
{
    float aspectRatio = (float)bitmap.Width / bitmap.Height;
    int newWidth = (int)(maxHeight * aspectRatio * offsetWidth);

    if (bitmap.Height > maxHeight || bitmap.Width > newWidth)
        bitmap = new Bitmap(bitmap, new Size(newWidth, maxHeight));
}

Bitmap FileToBitmap(string filePath)
{
    using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    return new Bitmap(fileStream);
}