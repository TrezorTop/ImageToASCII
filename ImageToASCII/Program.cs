using System.Drawing;
using System.Text;
using ImageToASCII;

const int maxHeight = 170;
const float offsetWidth = 2.5f;
char[] chars = { ' ', '.', ':', '+', '*', 'o', 'x', '8', '&', 'B', 'M', 'W', 'X', '$', '%', '#', '@' };
int charsLenght = chars.Length;
int charsStep = byte.MaxValue / charsLenght;
const string imagePath = @"C:\Users\User\Desktop\photo.jpg";

StringBuilder stringBuilder = new StringBuilder("");

Bitmap bitmap = Utilities.FileToBitmap(imagePath);

Utilities.ResizeBitmap(ref bitmap, maxHeight, offsetWidth);
bitmap.ToGrayScale();

bitmap.ProcessByRef(
    (scan0, offset) => {
        unsafe
        {
            int charIndex = ((byte*)scan0)[offset] / charsStep;

            if (charIndex >= charsLenght) charIndex = charsLenght - 1;

            stringBuilder.Append(chars[charIndex]);
        }
    },
    () => { stringBuilder.AppendLine(); }
);

Console.WriteLine(stringBuilder);
Console.ReadLine();