// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-28-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="TextResources.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SwarNet.Models;

/// <summary>
/// Class TextResources.
/// </summary>
public class TextResources
{
    /// <summary>
    /// Gets your turn text.
    /// </summary>
    /// <value>Your turn text.</value>
    public string YourTurnText { get; init; } = "Your turn! Send it!";
    /// <summary>
    /// Gets the opponent turn text.
    /// </summary>
    /// <value>The opponent turn text.</value>
    public string OpponentTurnText { get; init; } = "Brace for Impact!";
    /// <summary>
    /// Gets the stand by message.
    /// </summary>
    /// <value>The stand by message.</value>
    public string StandByMessage { get; init; } = "Stand by...";
    /// <summary>
    /// Gets the hit text.
    /// </summary>
    /// <value>The hit text.</value>
    public string HitText { get; init; } = "Hit!";
    /// <summary>
    /// Gets the miss text.
    /// </summary>
    /// <value>The miss text.</value>
    public string MissText { get; init; } = "Miss!";
    /// <summary>
    /// Gets the winner text.
    /// </summary>
    /// <value>The winner text.</value>
    public string WinnerText { get; init; } = "Victory!";
    /// <summary>
    /// Gets the loser text.
    /// </summary>
    /// <value>The loser text.</value>
    public string LoserText { get; init; } = "Defeat...";

}