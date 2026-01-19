using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SwarNet.Utilites
{
    public static class ExtensionMethods
    {
        public static string ToStringFromCamelCase(this Enum value)
        {
            var stringValue = value.ToString();

            if (string.IsNullOrEmpty(stringValue))
                return stringValue;

            return Regex.Replace(stringValue, @"(\B[A-Z])", " $1");
        }
    }
}
