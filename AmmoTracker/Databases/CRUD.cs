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
using System.Data;
using System.Data.Common;

namespace AmmoTracker.Databases;

/// <summary>
/// Class CRUD.
/// </summary>
/// <param name="connection">The connection.</param>
public class CRUD(SqliteConnection connection)
{

    #region Methods

    /// <summary>
    /// Adds the lot.
    /// </summary>
    /// <param name="typeId">The type identifier.</param>
    /// <param name="purchaseDate">The purchase date.</param>
    /// <param name="rounds">The rounds.</param>
    /// <param name="costPerRound">The cost per round.</param>
    public void AddLot(int typeId, string purchaseDate, int rounds, decimal costPerRound)
    {
        using var connection = new SqliteConnection(m_Connection?.ConnectionString);  // Fresh connection
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Lots (TypeID, PurchaseDate, Rounds, CostPerRound) 
                VALUES (@typeId, @date, @rounds, @cost)";
        cmd.Parameters.AddWithValue("@typeId", typeId);
        cmd.Parameters.AddWithValue("@date", purchaseDate);
        cmd.Parameters.AddWithValue("@rounds", rounds);
        cmd.Parameters.AddWithValue("@cost", costPerRound);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    /// <summary>
    /// Gets all lots with types.
    /// </summary>
    /// <param name="searchTerm">The search term.</param>
    /// <returns>DataTable.</returns>
    public DataTable GetAllLotsWithTypes(string? searchTerm = null)
    {
        var dt = new DataTable();

        using var connection = new SqliteConnection(m_Connection?.ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();

        var sql = @"
                SELECT l.LotID, t.Caliber, t.Manufacturer, t.Grain, l.PurchaseDate, l.Rounds, l.CostPerRound
                FROM Lots l 
                JOIN AmmoTypes t ON l.TypeID = t.TypeID";

        if (!string.IsNullOrEmpty(searchTerm))
        {
            sql += " WHERE t.Caliber LIKE @search OR t.Manufacturer LIKE @search OR t.Grain LIKE @search";
            cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
        }

        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        dt.Load(reader);

        connection.Close();
        return dt;
    }

    /// <summary>
    /// Gets the summary.
    /// </summary>
    /// <returns>System.ValueTuple{System.Int64, System.Int64}.</returns>
    public (long totalRounds, long lotCount) GetSummary()
    {
        using var connection = new SqliteConnection(m_Connection?.ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT SUM(Rounds), COUNT(*) FROM Lots";
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return (reader.GetInt64(0), reader.GetInt64(1));
        }
        connection.Close();
        return (0, 0);
    }

    /// <summary>
    /// Deletes the lot.
    /// </summary>
    /// <param name="lotId">The lot identifier.</param>
    public void DeleteLot(int lotId)
    {
        using var connection = new SqliteConnection(m_Connection?.ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM Lots WHERE LotID = @id";
        cmd.Parameters.AddWithValue("@id", lotId);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    /// <summary>
    /// Gets the ammo types.
    /// </summary>
    /// <returns>DataTable.</returns>
    public DataTable GetAmmoTypes()
    {
        var dt = new DataTable();

        using var conn = new SqliteConnection(m_Connection.ConnectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();

        var sql = @"
        SELECT TypeID, Caliber || ' ' || Grain || 'gr ' || Manufacturer AS Name
        FROM AmmoTypes";

        cmd.CommandText = sql;

        using var reader = cmd.ExecuteReader();
        dt.Load(reader);

        conn.Close();
        return dt;
    }

    /// <summary>
    /// Gets the low stock type count.
    /// </summary>
    /// <param name="threshold">The threshold.</param>
    /// <returns>System.Int32.</returns>
    public int GetLowStockTypeCount(int threshold = 500)
    {
        using var conn = new SqliteConnection(m_Connection.ConnectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
        SELECT COUNT(*) 
        FROM (
            SELECT SUM(Rounds) AS TotalRounds
            FROM Lots 
            GROUP BY TypeID 
            HAVING TotalRounds < @threshold
        )";

        cmd.Parameters.AddWithValue("@threshold", threshold);

        var lowCount = (int)cmd.ExecuteScalar();

        conn.Close();
        return lowCount;
    }

    /// <summary>
    /// Updates the lot.
    /// </summary>
    /// <param name="lotId">The lot identifier.</param>
    /// <param name="typeId">The type identifier.</param>
    /// <param name="purchaseDate">The purchase date.</param>
    /// <param name="rounds">The rounds.</param>
    /// <param name="costPerRound">The cost per round.</param>
    public void UpdateLot(int lotId, int typeId, string purchaseDate, int rounds, decimal costPerRound)
    {
        using var conn = new SqliteConnection(m_Connection.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
        UPDATE Lots 
        SET TypeID = @typeId, 
            PurchaseDate = @date, 
            Rounds = @rounds, 
            CostPerRound = @cost 
        WHERE LotID = @lotId";
        cmd.Parameters.AddWithValue("@lotId", lotId);
        cmd.Parameters.AddWithValue("@typeId", typeId);
        cmd.Parameters.AddWithValue("@date", purchaseDate);
        cmd.Parameters.AddWithValue("@rounds", rounds);
        cmd.Parameters.AddWithValue("@cost", costPerRound);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    #endregion

    #region Properties & Fields

    /// <summary>
    /// The m connection
    /// </summary>
    private readonly SqliteConnection? m_Connection = connection;

    #endregion
}