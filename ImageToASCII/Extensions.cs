using System.Drawing;
using System.Drawing.Imaging;


public static class Extensions
{
    public static void ToGrayScale(this Bitmap bitmap)
    {
        bitmap.ProcessByRef((scan0, offset) =>
        {
            unsafe
            {
                byte* pointer = (byte*)scan0;

                byte red = pointer[offset + 2];
                byte green = pointer[offset + 1];
                byte blue = pointer[offset];

                byte gray = (byte)((red + green + blue) / 3);

                pointer[offset] = gray;
                pointer[offset + 1] = gray;
                pointer[offset + 2] = gray;
            }
        });
    }

    public static void ProcessByRef(this Bitmap bitmap, Action<IntPtr, int>? onPixelProcess, Action? onRowProcess = null)
    {
        Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

        int stride = bitmapData.Stride; // number of bytes allocated for each scan line

        try
        {
            unsafe
            {
                IntPtr scan0 = bitmapData.Scan0;

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int offset = y * stride + x * 4; // pixel is represented by four bytes (RGBA)

                        onPixelProcess?.Invoke(scan0, offset);
                    }

                    onRowProcess?.Invoke();
                }
            }
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }
    }
}