using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            result = result.AddSeconds(unixTimeStamp).ToLocalTime();
            return result;
        }
    }
}
