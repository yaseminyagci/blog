using System;
using System.Globalization;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        //public static DateTime ToCustomDateTime(this string s, string format = Constants.GeneralConstants.DefaultDateFormat, string cultureString = "tr-TR")
        //{
        //    var r = DateTime.ParseExact(s, format, CultureInfo.GetCultureInfo(cultureString));
        //    return r;
        //}

        //public static DateTime ToCustomDateTime(this string s, string format, CultureInfo culture)
        //{
        //    var r = DateTime.ParseExact(s, format, culture);
        //    return r;
        //}
        public static String ToTimeStamp(this DateTime dt)
        {
            return dt.ToString("MMddyyyyHHmmssffff");
        }
        public static DateTime ToCustomDateTime(this string s, string format = Constants.GeneralConstants.DefaultDateFormat)
        {
            var date = DateTime.TryParseExact(s: s, format: format, provider: null, style: 0, out var dt)
                ? dt : DateTime.Now;
            return date;
        }

        public static string ToCustomFormatString(this DateTime dt, CultureInfo info, bool isTimeExist = false)
        {

            return isTimeExist ? dt.ToString(Constants.GeneralConstants.DefaultDateAndTimeFormat, info) : dt.ToString(Constants.GeneralConstants.DefaultDateFormat, info);
        }
        public static string ToCustomFormatString(this DateTime dt, string format, CultureInfo info)
        {

            return dt.ToString(format, info);
        }

        public static string ToCustomFormatString(this DateTime dt, bool isTimeExist = false)
        {
            return isTimeExist
                ? dt.ToString(Constants.GeneralConstants.DefaultDateAndTimeFormat, CultureInfo.InvariantCulture)
                : dt.ToString(Constants.GeneralConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
        }

    }

}
