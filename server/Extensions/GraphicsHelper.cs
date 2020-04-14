namespace Charity.Extensions
{
    using System;
    using System.Drawing;

    public static class GraphicsHelper
    {
        public static Image ResizeImage(Image image, int width, int height)
        {
            return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }
    }
}
