using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace VKClient.Converters
{
    public static class HexToColorConverter
    {
        public static SolidColorBrush GetColorFromHex(string hexColor)
        {
            hexColor = hexColor.Replace("#", "");
            byte a = byte.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte r = byte.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hexColor.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
    }
}
