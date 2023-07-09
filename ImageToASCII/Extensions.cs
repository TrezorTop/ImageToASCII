using System.Drawing;
using System.Drawing.Imaging;

public static class Extensions
{
    public static void ToGrayScale(this Bitmap bitmap)
    {
        Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
        
        int stride = bitmapData.Stride;

        try
        {
            unsafe
            {
                byte* pointer = (byte*)bitmapData.Scan0;

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int offset = y * stride + x * 4;

                        byte blue = pointer[offset];
                        byte green = pointer[offset + 1];
                        byte red = pointer[offset + 2];

                        byte gray = (byte)((red + green + blue) / 3);

                        pointer[offset] = gray;
                        pointer[offset + 1] = gray;
                        pointer[offset + 2] = gray;
                    }
                }
            }
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }
    }
}
