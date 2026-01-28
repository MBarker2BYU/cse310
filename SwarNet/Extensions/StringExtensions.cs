using System.Text.RegularExpressions;

namespace SwarNet.Extensions;

public static class StringExtensions
{
    public static string ToStringFromCamelCase(this Enum value)
    {
        var stringValue = value.ToString();

        if (string.IsNullOrEmpty(stringValue))
            return stringValue;

        return Regex.Replace(stringValue, @"(\B[A-Z])", " $1");
    }
}