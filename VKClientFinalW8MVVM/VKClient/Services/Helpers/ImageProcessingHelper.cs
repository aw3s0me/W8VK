using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace VKClient.Services.Helpers
{
    public static class ImageProcessingHelper
    {
        static public long ScaleImageWidth(long oldWidth, long oldHeight, long maxWidth, long maxHeight)
        {
            var ratioX = (double)maxWidth / oldWidth;
            var ratioY = (double)maxHeight / oldHeight;
            var ratio = Math.Min(ratioX, ratioY);

            return (long)(oldWidth * ratio);
        }
        static public long ScaleImageHeight(long oldWidth, long oldHeight, long maxWidth, long maxHeight)
        {
            var ratioX = (double)maxWidth / oldWidth;
            var ratioY = (double)maxHeight / oldHeight;
            var ratio = Math.Min(ratioX, ratioY);

            return (long)(oldHeight * ratio);
        } 
    }
}
