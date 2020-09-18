using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Constants;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool isEmail(this string s)
        {
            var emailRegex = RegexConstants.EmailRegex;
            var re = new Regex(emailRegex);
            return re.IsMatch(s);

        }

        public static bool isUsername(this string s)
        {
            var usernameRegex = RegexConstants.UserNameRegex;
            var re = new Regex(usernameRegex);
            return re.IsMatch(s);
        }
        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
