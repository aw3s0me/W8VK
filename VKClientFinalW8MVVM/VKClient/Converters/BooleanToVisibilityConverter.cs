using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace VKClient.Converters
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool flag = false;
            if (value is bool ? (bool) value : false)
            {
                flag = (bool)value;
            }
            else
            {
                if (value is bool?)
                {
                    flag = ((bool?)value).GetValueOrDefault();
                }
                else
                {
                    if (value is string)
                    {
                        bool.TryParse((string)value, out flag);
                    }
                }
            }
            bool flag2;
            if (parameter != null && bool.TryParse((string)parameter, out flag2) && flag2)
            {
                flag = !flag;
            }
            if (flag)
            {
                return 0;
            }
            return 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool flag = value is Visibility && (Visibility)value == 0;
            if (parameter != null && (bool)parameter)
            {
                flag = !flag;
            }
            return flag;
        }
    }
}
