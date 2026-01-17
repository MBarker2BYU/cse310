// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="TimeDuration.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary>Custom value type for time durations</summary>
// ***********************************************************************
namespace CodeTimeTracker.Data.Models;

public struct TimeDuration
{
    public int Hours { get; }
    public int Minutes { get; }
    public int Seconds { get; }

    public TimeDuration(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    /// <summary>
    /// Creates TimeDuration from TimeSpan (handles days rollover correctly)
    /// </summary>
    public TimeDuration(TimeSpan span)
        : this(
            span.Days * 24 + span.Hours,
            span.Minutes,
            span.Seconds)
    {
    }

    public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
}