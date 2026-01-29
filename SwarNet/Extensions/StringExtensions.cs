// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text.RegularExpressions;

namespace SwarNet.Extensions;

/// <summary>
/// Class StringExtensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts to stringfromcamelcase.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>System.String.</returns>
    public static string ToStringFromCamelCase(this Enum value)
    {
        var stringValue = value.ToString();

        if (string.IsNullOrEmpty(stringValue))
            return stringValue;

        return Regex.Replace(stringValue, @"(\B[A-Z])", " $1");
    }
}