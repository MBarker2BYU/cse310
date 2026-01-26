using System.Text.RegularExpressions;

namespace SwarNet.Utilities
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
