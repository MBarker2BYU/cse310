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

using AmmoTracker.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace AmmoTracker.Databases;

/// <summary>
/// Class CRUD.
/// </summary>
/// <param name="connection">The connection.</param>
public class CRUD(SqliteConnection connection)
{
    #region Mehtods

    // Add a new Manufacturer
    public long AddManufacturer(string manufacturerName)
    {
        if (string.IsNullOrWhiteSpace(manufacturerName))
            throw new ArgumentException("Manufacturer name cannot be empty.");

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Manufacturers (ManufacturerName)
                VALUES (@name);
                SELECT last_insert_rowid();";

        cmd.Parameters.AddWithValue("@name", manufacturerName.Trim());

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    // Add a new Caliber
    public long AddCaliber(string caliberName)
    {
        if (string.IsNullOrWhiteSpace(caliberName))
            throw new ArgumentException("Caliber name cannot be empty.");

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Calibers (CaliberName)
                VALUES (@name);
                SELECT last_insert_rowid();";

        cmd.Parameters.AddWithValue("@name", caliberName.Trim());

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    // Add a new Grain
    public long AddGrain(string grainValue)
    {
        if (string.IsNullOrWhiteSpace(grainValue))
            throw new ArgumentException("Grain value cannot be empty.");

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Grains (GrainValue)
                VALUES (@value);
                SELECT last_insert_rowid();";

        cmd.Parameters.AddWithValue("@value", grainValue.Trim());

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    // CREATE: Add a new purchase
    public long AddPurchase(long typeId, DateTime purchaseDate, long roundsAdded, long roundsPerContainer, long containers, string? lotNumber, decimal costPerRound)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Purchases (TypeID, PurchaseDate, RoundsAdded, RoundsPerContainer, Containers, LotNumber, CostPerRound)
                VALUES (@typeId, @date, @roundsAdded, @roundsPer, @containers, @lot, @cost);
                SELECT last_insert_rowid();";

        cmd.Parameters.AddWithValue("@typeId", typeId);
        cmd.Parameters.AddWithValue("@date", purchaseDate.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@roundsAdded", roundsAdded);
        cmd.Parameters.AddWithValue("@roundsPer", roundsPerContainer);
        cmd.Parameters.AddWithValue("@containers", containers);
        cmd.Parameters.AddWithValue("@lot", (object?)lotNumber ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@cost", costPerRound);

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    /// <summary>
    /// Retrieves a single purchase by its PurchaseID.
    /// </summary>
    /// <param name="purchaseId">The ID of the purchase to retrieve.</param>
    /// <returns>A PurchaseItem object if found, otherwise null.</returns>
    public PurchaseItem? GetPurchaseById(long purchaseId)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        SELECT 
            PurchaseID,
            TypeID,
            PurchaseDate,
            RoundsAdded,
            RoundsPerContainer,
            Containers,
            LotNumber,
            CostPerRound,
            (RoundsAdded * CostPerRound) AS TotalCost
        FROM Purchases
        WHERE PurchaseID = @purchaseId";

        cmd.Parameters.AddWithValue("@purchaseId", purchaseId);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new PurchaseItem
            {
                PurchaseID = reader.GetInt64(0),
                TypeID = reader.GetInt64(1),  // Include TypeID if needed for context
                PurchaseDate = DateTime.Parse(reader.GetString(2)),
                RoundsAdded = reader.GetInt64(3),
                RoundsPerContainer = reader.GetInt64(4),
                Containers = reader.GetInt64(5),
                LotNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                CostPerRound = reader.GetDecimal(7),
                TotalCost = reader.GetDecimal(8)
            };
        }

        return null;
    }

    /// <summary>
    /// Deletes a specific purchase record by its PurchaseID.
    /// </summary>
    /// <param name="purchaseId">The ID of the purchase to delete.</param>
    public void DeletePurchaseById(long purchaseId)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        DELETE FROM Purchases
        WHERE PurchaseID = @purchaseId";

        cmd.Parameters.AddWithValue("@purchaseId", purchaseId);

        cmd.ExecuteNonQuery();
    }

    // READ: Get aggregated inventory (strong-typed list)
    public List<InventoryItem> GetInventoryByType()
    {
        var results = new List<InventoryItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                SELECT
                    t.TypeID,
                    m.ManufacturerName,
                    c.CaliberName,
                    g.GrainValue,
                    COALESCE(SUM(p.RoundsAdded), 0) AS CurrentRounds,
                    t.MinimumThreshold,
                    CASE
                        WHEN COALESCE(SUM(p.RoundsAdded), 0) < t.MinimumThreshold THEN 'Low'
                        ELSE 'OK'
                    END AS Status,
                    ROUND(COALESCE(SUM(p.RoundsAdded * p.CostPerRound), 0), 2) AS TotalValue
                FROM AmmoTypes t
                LEFT JOIN Purchases p ON t.TypeID = p.TypeID
                INNER JOIN Manufacturers m ON t.ManufacturerID = m.ManufacturerID
                INNER JOIN Calibers c ON t.CaliberID = c.CaliberID
                INNER JOIN Grains g ON t.GrainID = g.GrainID
                GROUP BY t.TypeID, m.ManufacturerName, c.CaliberName, g.GrainValue, t.MinimumThreshold
                ORDER BY CurrentRounds DESC;";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new InventoryItem
            {
                TypeID = reader.GetInt64(0),
                ManufacturerName = reader.GetString(1),
                CaliberName = reader.GetString(2),
                GrainValue = reader.GetString(3),
                CurrentRounds = reader.GetInt64(4),
                MinimumThreshold = reader.GetInt64(5),
                Status = reader.GetString(6),
                TotalValue = reader.GetDecimal(7)
            });
        }

        return results;
    }

    // READ: Get purchases for a specific type (strong-typed list)
    public List<PurchaseItem> GetPurchasesByTypeId(long typeId)
    {
        var results = new List<PurchaseItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                SELECT 
                    PurchaseID,
                    PurchaseDate,
                    RoundsAdded,
                    RoundsPerContainer,
                    Containers,
                    LotNumber,
                    CostPerRound,
                    (RoundsAdded * CostPerRound) AS TotalCost
                FROM Purchases
                WHERE TypeID = @typeId
                ORDER BY PurchaseDate DESC";

        cmd.Parameters.AddWithValue("@typeId", typeId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new PurchaseItem
            {
                PurchaseID = reader.GetInt64(0),
                PurchaseDate = DateTime.Parse(reader.GetString(1)),
                RoundsAdded = reader.GetInt64(2),
                RoundsPerContainer = reader.GetInt64(3),
                Containers = reader.GetInt64(4),
                LotNumber = reader.IsDBNull(5) ? null : reader.GetString(5),
                CostPerRound = reader.GetDecimal(6),
                TotalCost = reader.GetDecimal(7)
            });
        }

        return results;
    }

    // UPDATE: Modify an existing purchase
    public void UpdatePurchase(PurchaseItem purchase)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                UPDATE Purchases
                SET TypeID            = @typeId, 
                    PurchaseDate       = @date,
                    RoundsAdded        = @roundsAdded,
                    RoundsPerContainer = @roundsPer,
                    Containers         = @containers,
                    LotNumber          = @lot,
                    CostPerRound       = @cost
                WHERE PurchaseID = @id";

        cmd.Parameters.AddWithValue("@id", purchase.PurchaseID);
        cmd.Parameters.AddWithValue("@typeId", purchase.TypeID);
        cmd.Parameters.AddWithValue("@date", purchase.PurchaseDate.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@roundsAdded", purchase.RoundsAdded);
        cmd.Parameters.AddWithValue("@roundsPer", purchase.RoundsPerContainer);
        cmd.Parameters.AddWithValue("@containers", purchase.Containers);
        cmd.Parameters.AddWithValue("@lot", (object?)purchase.LotNumber ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@cost", purchase.CostPerRound);

        cmd.ExecuteNonQuery();
    }
    
    // UPDATE: Update MinimumThreshold on an AmmoType
    public void UpdateMinimumThreshold(long typeId, long newThreshold)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                UPDATE AmmoTypes
                SET MinimumThreshold = @threshold
                WHERE TypeID = @id";

        cmd.Parameters.AddWithValue("@id", typeId);
        cmd.Parameters.AddWithValue("@threshold", newThreshold);

        cmd.ExecuteNonQuery();
    }

    // Get single AmmoType details
    public AmmoTypeDetail? GetAmmoTypeById(long typeId)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
                SELECT TypeID, ManufacturerID, CaliberID, GrainID, MinimumThreshold
                FROM AmmoTypes
                WHERE TypeID = @id";

        cmd.Parameters.AddWithValue("@id", typeId);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new AmmoTypeDetail
            {
                TypeID = reader.GetInt64(0),
                ManufacturerID = reader.GetInt64(1),
                CaliberID = reader.GetInt64(2),
                GrainID = reader.GetInt64(3),
                MinimumThreshold = reader.GetInt64(4)
            };
        }

        return null;
    }

    // Check if value already exists (used in add forms)
    public bool ValueAlreadyExists(string tableName, string columnName, string value)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = $"SELECT 1 FROM {tableName} WHERE {columnName} = @val COLLATE NOCASE";
        cmd.Parameters.AddWithValue("@val", value);

        return cmd.ExecuteScalar() != null;
    }

    // Insert new value and return ID (used in add forms)
    public long InsertNewValue(string tableName, string columnName, string value)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = $"INSERT INTO {tableName} ({columnName}) VALUES (@val); SELECT last_insert_rowid();";
        cmd.Parameters.AddWithValue("@val", value);

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    // Get summary stats
    public (long TotalRounds, long LowStockTypes) GetSummaryStats()
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();

        cmd.CommandText = "SELECT COALESCE(SUM(RoundsAdded), 0) FROM Purchases";
        var totalRounds = Convert.ToInt64(cmd.ExecuteScalar());

        cmd.CommandText = @"
                SELECT COUNT(*)
                FROM AmmoTypes t
                LEFT JOIN Purchases p ON t.TypeID = p.TypeID
                GROUP BY t.TypeID
                HAVING COALESCE(SUM(p.RoundsAdded), 0) < t.MinimumThreshold";
        var lowStockTypes = Convert.ToInt64(cmd.ExecuteScalar());

        return (totalRounds, lowStockTypes);
    }

    // GetOrCreateAmmoType (updated to long)
    public long GetOrCreateAmmoType(long manufacturerId, long caliberId, long grainId)
    {
        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();

        cmd.CommandText = @"
                SELECT TypeID FROM AmmoTypes
                WHERE ManufacturerID = @manu
                  AND CaliberID = @cal
                  AND GrainID = @grain";
        cmd.Parameters.AddWithValue("@manu", manufacturerId);
        cmd.Parameters.AddWithValue("@cal", caliberId);
        cmd.Parameters.AddWithValue("@grain", grainId);

        var result = cmd.ExecuteScalar();
        if (result != null && result != DBNull.Value)
            return Convert.ToInt64(result);

        cmd.Parameters.Clear();
        cmd.CommandText = @"
                INSERT INTO AmmoTypes (ManufacturerID, CaliberID, GrainID)
                VALUES (@manu, @cal, @grain);
                SELECT last_insert_rowid();";
        cmd.Parameters.AddWithValue("@manu", manufacturerId);
        cmd.Parameters.AddWithValue("@cal", caliberId);
        cmd.Parameters.AddWithValue("@grain", grainId);

        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    public List<ComboItem> GetManufacturers()
    {
        var results = new List<ComboItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        SELECT ManufacturerID, ManufacturerName 
        FROM Manufacturers 
        ORDER BY ManufacturerName ASC";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new ComboItem
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1)
            });
        }

        return results;
    }

    // Returns list for Calibers combo
    public List<ComboItem> GetCalibers()
    {
        var results = new List<ComboItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        SELECT CaliberID, CaliberName 
        FROM Calibers 
        ORDER BY CaliberName ASC";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new ComboItem
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1)
            });
        }

        return results;
    }

    // Returns list for Grains combo
    public List<ComboItem> GetGrains()
    {
        var results = new List<ComboItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        SELECT GrainID, GrainValue 
        FROM Grains 
        ORDER BY GrainValue ASC";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new ComboItem
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1)
            });
        }

        return results;
    }

    public List<string> GetDistinctLotNumbers()
    {
        var results = new List<string>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();
        cmd.CommandText = @"
        SELECT DISTINCT LotNumber
        FROM Purchases
        WHERE LotNumber IS NOT NULL AND LotNumber != ''
        ORDER BY LotNumber ASC";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }

    /// <summary>
    /// Gets the aggregated inventory filtered by any combination of manufacturer, caliber, grain, date range, and lot number.
    /// </summary>
    /// <param name="filter">The filter criteria (null or empty fields are ignored).</param>
    /// <returns>List of filtered InventoryItem objects.</returns>
    public List<InventoryItem> GetInventoryByFilter(InventoryFilter filter)
    {
        var results = new List<InventoryItem>();

        if (m_Connection!.State != ConnectionState.Open)
            m_Connection.Open();

        using var cmd = m_Connection.CreateCommand();

        cmd.CommandText = @"
        SELECT
            t.TypeID,
            m.ManufacturerName,
            c.CaliberName,
            g.GrainValue,
            COALESCE(SUM(p.RoundsAdded), 0) AS CurrentRounds,
            t.MinimumThreshold,
            CASE
                WHEN COALESCE(SUM(p.RoundsAdded), 0) < t.MinimumThreshold THEN 'Low'
                ELSE 'OK'
            END AS Status,
            ROUND(COALESCE(SUM(p.RoundsAdded * p.CostPerRound), 0), 2) AS TotalValue
        FROM AmmoTypes t
        LEFT JOIN Purchases p ON t.TypeID = p.TypeID
        INNER JOIN Manufacturers m ON t.ManufacturerID = m.ManufacturerID
        INNER JOIN Calibers c ON t.CaliberID = c.CaliberID
        INNER JOIN Grains g ON t.GrainID = g.GrainID
        WHERE 1=1";

        // Manufacturer
        if (filter?.ManufacturerId.HasValue == true && filter.ManufacturerId.Value > 0)
        {
            cmd.CommandText += " AND m.ManufacturerID = @manu";
            cmd.Parameters.AddWithValue("@manu", filter.ManufacturerId.Value);
        }

        // Caliber
        if (filter?.CaliberId.HasValue == true && filter.CaliberId.Value > 0)
        {
            cmd.CommandText += " AND c.CaliberID = @cal";
            cmd.Parameters.AddWithValue("@cal", filter.CaliberId.Value);
        }

        // Grain
        if (filter?.GrainId.HasValue == true && filter.GrainId.Value > 0)
        {
            cmd.CommandText += " AND g.GrainID = @grain";
            cmd.Parameters.AddWithValue("@grain", filter.GrainId.Value);
        }

        // Date range (on Purchases)
        if (filter?.StartDate.HasValue == true)
        {
            cmd.CommandText += " AND p.PurchaseDate >= @start";
            cmd.Parameters.AddWithValue("@start", filter.StartDate.Value.ToString("yyyy-MM-dd"));
        }

        if (filter?.EndDate.HasValue == true)
        {
            cmd.CommandText += " AND p.PurchaseDate <= @end";
            cmd.Parameters.AddWithValue("@end", filter.EndDate.Value.ToString("yyyy-MM-dd"));
        }

        // Lot number (partial match)
        if (!string.IsNullOrWhiteSpace(filter?.LotNumber))
        {
            cmd.CommandText += " AND p.LotNumber LIKE @lot";
            cmd.Parameters.AddWithValue("@lot", "%" + filter.LotNumber.Trim() + "%");
        }

        cmd.CommandText += @"
        GROUP BY t.TypeID, m.ManufacturerName, c.CaliberName, g.GrainValue, t.MinimumThreshold
        ORDER BY CurrentRounds DESC;";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new InventoryItem
            {
                TypeID = reader.GetInt64(0),
                ManufacturerName = reader.GetString(1),
                CaliberName = reader.GetString(2),
                GrainValue = reader.GetString(3),
                CurrentRounds = reader.GetInt64(4),
                MinimumThreshold = reader.GetInt64(5),
                Status = reader.GetString(6),
                TotalValue = reader.GetDecimal(7)
            });
        }

        return results;
    }

    #endregion

    #region Properties & Fields

    /// <summary>
    /// The m connection
    /// </summary>
    private readonly SqliteConnection? m_Connection = connection;

    #endregion
}