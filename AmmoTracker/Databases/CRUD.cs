// ***********************************************************************
// Assembly        : 
// Author           : Matthew D. Barker
// Created          : 01-28-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="CRUD.cs" company="ShadowWorx Systems, LLC">
//     Copyright (c) Matthew D Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Data.Sqlite;

namespace AmmoTracker.Databases;

/// <summary>
/// Class CRUD.
/// </summary>
/// <param name="connection">The connection.</param>
public class CRUD(SqliteConnection connection)
{


    #region Properties & Fields

    /// <summary>
    /// The m connection
    /// </summary>
    private readonly SqliteConnection? m_Connection = connection;

    #endregion
}