// ***********************************************************************
// Assembly         : 
// Author           : Matthew D. Barker
// Created          : 01-28-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="Database.cs" company="ShadowWorx Systems, LLC">
//     Copyright (c) Matthew D Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace AmmoTracker.Databases;

/// <summary>
/// Class Database.
/// </summary>
public class Database
{
    /// <summary>
    /// The m database path
    /// </summary>
    private readonly string m_DbPath = "AmmoTracker.db";
    /// <summary>
    /// The m connection
    /// </summary>
    private readonly SqliteConnection? m_Connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    public Database()
    {
        m_Connection = new SqliteConnection($"Data Source={m_DbPath}");
        CRUD = new CRUD(m_Connection);
        CreateDatabase();
    }

    /// <summary>
    /// Creates the database.
    /// </summary>
    public void CreateDatabase()
    {
        m_Connection.Open();
        using var transaction = m_Connection.BeginTransaction();  // Optional: atomic creation
        using var cmd = m_Connection.CreateCommand();

        // Table 1: AmmoTypes
        cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS AmmoTypes (
                    TypeID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Caliber TEXT NOT NULL,
                    Grain REAL,
                    Manufacturer TEXT
                )";
        cmd.ExecuteNonQuery();

        // Table 2: Lots
        cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Lots (
                    LotID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TypeID INTEGER NOT NULL,
                    PurchaseDate TEXT,
                    Rounds INTEGER NOT NULL,
                    CostPerRound REAL,
                    FOREIGN KEY(TypeID) REFERENCES AmmoTypes(TypeID)
                )";
        cmd.ExecuteNonQuery();

        // Seed example types if empty
        cmd.CommandText = "SELECT COUNT(*) FROM AmmoTypes";
        var count = (long)(cmd.ExecuteScalar() ?? 0L);

        if (count == 0)
        {
            cmd.CommandText = @"
                    INSERT INTO AmmoTypes (Caliber, Grain, Manufacturer) 
                    VALUES 
                        ('9mm', 124, 'Federal'),
                        ('5.56', 55, 'PMC')";
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
        m_Connection.Close();
    }

    

    public CRUD CRUD { get; }
}