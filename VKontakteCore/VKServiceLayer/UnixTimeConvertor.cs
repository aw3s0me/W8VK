using System;

namespace VKServiceLayer 
{

    public class UnixTimeConvertor
    {
        public static DateTime ConvertFromUnixTimestampDouble(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static DateTime ConvertFromUnixTimestampString(string timestamp)
        {
            return ConvertFromUnixTimestampDouble(Double.Parse(timestamp));
        }
    }
}
