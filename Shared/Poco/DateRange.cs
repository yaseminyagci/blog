using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Core.Constants;
using Core.Extensions;

namespace Shared.Poco
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string ToString(string seperator, string format, CultureInfo info)
        {
            return $"{StartDate.ToCustomFormatString(format, info)} {seperator} {EndDate.ToCustomFormatString(format, info)}";
        }

        public TimeSpan Substract()
        {
            return EndDate.Subtract(StartDate);
        }
    }
}
