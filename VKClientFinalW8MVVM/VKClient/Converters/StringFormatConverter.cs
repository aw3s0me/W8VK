using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace VKClient.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {
                string formatString = parameter.ToString();
                if (!string.IsNullOrEmpty(formatString))
                {
                    string strValue = value as string;
                    if (strValue != null && !string.IsNullOrWhiteSpace(strValue))
                    {
                        return String.Format(formatString, strValue);
                    }
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        #endregion
    };
}
