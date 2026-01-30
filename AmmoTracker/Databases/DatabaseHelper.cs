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

using System.Data;
using Microsoft.Data.Sqlite;

namespace AmmoTracker.Databases;

/// <summary>
/// Class Database.
/// </summary>
public class DatabaseHelper
{
    /// <summary>
    /// The database path
    /// </summary>
    private readonly string m_DbPath = "AmmoTracker.db";
    /// <summary>
    /// The SqliteConnection provides access to the database
    /// </summary>
    private readonly SqliteConnection? m_Connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseHelper"/> class.
    /// </summary>
    public DatabaseHelper()
    {
        m_Connection = new SqliteConnection($"Data Source={m_DbPath}");
        CRUD = new CRUD(m_Connection);
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        m_Connection!.Open();

        using var transaction = m_Connection.BeginTransaction();
        using var cmd = m_Connection.CreateCommand();

        try
        {
            // Manufacturers
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Manufacturers (
                    ManufacturerID   INTEGER PRIMARY KEY AUTOINCREMENT,
                    ManufacturerName TEXT    NOT NULL UNIQUE
                );";
            cmd.ExecuteNonQuery();

            // Calibers
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Calibers (
                    CaliberID   INTEGER PRIMARY KEY AUTOINCREMENT,
                    CaliberName TEXT    NOT NULL UNIQUE
                );";
            cmd.ExecuteNonQuery();

            // Grains
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Grains (
                    GrainID     INTEGER PRIMARY KEY AUTOINCREMENT,
                    GrainValue  TEXT    NOT NULL UNIQUE
                );";
            cmd.ExecuteNonQuery();

            // AmmoTypes (junction table)
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS AmmoTypes (
                    TypeID          INTEGER PRIMARY KEY AUTOINCREMENT,
                    ManufacturerID  INTEGER NOT NULL,
                    CaliberID       INTEGER NOT NULL,
                    GrainID         INTEGER NOT NULL,
                    MinimumThreshold INTEGER DEFAULT 0 NOT NULL,              -- user sets min rounds to keep
                    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID),
                    FOREIGN KEY (CaliberID)      REFERENCES Calibers(CaliberID),
                    FOREIGN KEY (GrainID)        REFERENCES Grains(GrainID),
                    UNIQUE(ManufacturerID, CaliberID, GrainID)
                );";
            cmd.ExecuteNonQuery();

            // Lots
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Purchases (
                    PurchaseID     INTEGER PRIMARY KEY AUTOINCREMENT,
                    TypeID         INTEGER NOT NULL,
                    PurchaseDate   TEXT    NOT NULL,                -- ISO YYYY-MM-DD
                    RoundsAdded    INTEGER NOT NULL CHECK(RoundsAdded > 0),
                    RoundsPerContainer INTEGER NOT NULL CHECK(RoundsPerContainer > 0),
                    Containers     INTEGER NOT NULL DEFAULT 1,
                    LotNumber      TEXT,                            -- optional text field for lot/batch
                    CostPerRound   REAL    NOT NULL CHECK(CostPerRound >= 0),
                    FOREIGN KEY (TypeID) REFERENCES AmmoTypes(TypeID)
                );";
            cmd.ExecuteNonQuery();

            // Index on Purchases.TypeID (speeds up queries filtering or joining on TypeID)
            cmd.CommandText = @"
            CREATE INDEX IF NOT EXISTS idx_purchases_typeid ON Purchases(TypeID);";
            cmd.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;  // Surface to caller so startup can fail loudly if needed
        }
        finally
        {
            if (m_Connection.State == ConnectionState.Open)
                m_Connection.Close();
        }
    }

    public CRUD CRUD { get; }
}