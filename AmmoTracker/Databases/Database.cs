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

    private void CreateDatabase()
    {
        try
        {
            m_Connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            if(m_Connection.State == ConnectionState.Open) 
                m_Connection.Close();
        }

    }


    public CRUD CRUD { get; }
}